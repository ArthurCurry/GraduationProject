using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 来回移动平台的控制
/// </summary>
public class ControlContinuedMove : AllMechanism
{

    [Tooltip("平台移动的结束位置")]
    public Vector3 stopPosition;
    [Tooltip("平台移动的结束位置")]
    public Vector3 stopPosition2;
    [Tooltip("平台移动的结束位置")]
    public Vector3 stopPosition3;
    [Tooltip("平台移动的结束位置")]
    public Vector3 stopPosition4;
    [Tooltip("平台到边界后的停留时间")]
    public float stayTime = 1f;
    [Tooltip("平台移动一次的时间")]
    public float moveTime = 1f;
    [Tooltip("延迟执行")]
    public float delayTime = 1f;
    [Tooltip("对应机关")]
    public GameObject mechanism;

    private bool toStart = false;         // 是否朝开始位置移动
    private bool toStop = false;         // 是否朝结束位置移动
    private bool arrive0 = true;         // 是否到达位置0（起点）
    private bool arrive1 = false;         // 是否到达位置1
    private bool arrive2 = false;         // 是否到达位置2
    private bool arrive3 = false;         // 是否到达位置3
    private bool arrive4 = false;         // 是否到达位置4（终点）
    private float speed;                // 移动的速度
    private Vector3 startPosition;       // 开始位置

    internal bool on = true;           // 平台移动开关，是否允许平台移动
    void Start()
    {
        startPosition = transform.position;
        speed = Vector3.Distance(transform.position, stopPosition) / moveTime;
    }
    void Update()
    {
        on = mechanism.GetComponent<MoveMechanism>().implement;  // 接受控制机关的指令
        if (on)                    // 该部分是为了让他不自己乱动，控制机关给一次命令，就动一次
        {
            toStop = true;
            toStart = false;
        }
        else if (!on)
        {
            toStop = false;
            toStart = true;
        }
        PlatformMoveOn(true);
    }

    /// <summary>
    /// 平台移动控制
    /// </summary>
    /// <param name="on">平台移动开关</param>
    void PlatformMoveOn(bool on)
    {
        if (!on) { return; }
        StartCoroutine(PlatformMove());
    }

    /// <summary>
    /// 具体平台移动控制
    /// </summary>
    /// <param name="stopPosition">停止位置</param>
    /// <returns></returns>
    IEnumerator PlatformMove()
    {
        yield return new WaitForSeconds(delayTime);
        Vector3 tempPosition = transform.position;
        if (arrive0)
        {
            if (toStop)
            {
                tempPosition = Vector3.MoveTowards(tempPosition, stopPosition, speed * Time.deltaTime);
                transform.position = tempPosition;
                if (transform.position == stopPosition)
                {
                    arrive0 = false;
                    yield return new WaitForSeconds(stayTime);
                    arrive1 = true;
                }
            }
        }

        if (arrive1)
        {
            if (toStop)
            {
                tempPosition = Vector3.MoveTowards(tempPosition, stopPosition2, speed * Time.deltaTime);
                transform.position = tempPosition;
                if (transform.position == stopPosition2)
                {
                    arrive1 = false;
                    yield return new WaitForSeconds(stayTime);
                    arrive2 = true;

                }
            }
            else if (toStart)
            {
                tempPosition = Vector3.MoveTowards(tempPosition, startPosition, speed * Time.deltaTime);
                transform.position = tempPosition;
                if (transform.position == startPosition)
                {
                    arrive1 = false;
                    yield return new WaitForSeconds(stayTime);
                    arrive0 = true;

                }
            }

        }

        if (arrive2)
        {
            if (toStop)
            {
                tempPosition = Vector3.MoveTowards(tempPosition, stopPosition3, speed * Time.deltaTime);
                transform.position = tempPosition;
                if (transform.position == stopPosition3)
                {
                    arrive2 = false;
                    yield return new WaitForSeconds(stayTime);            
                    arrive3 = true;

                }
            }
            else if(toStart)
            {
                tempPosition = Vector3.MoveTowards(tempPosition, stopPosition, speed * Time.deltaTime);
                transform.position = tempPosition;
                if (transform.position == stopPosition)
                {
                    arrive2 = false;
                    yield return new WaitForSeconds(stayTime);
                    arrive1 = true;

                }
            }
        }

        if (arrive3)
        {
            if (toStop)
            {
                tempPosition = Vector3.MoveTowards(tempPosition, stopPosition4, speed * Time.deltaTime);
                transform.position = tempPosition;
                if (transform.position == stopPosition4)
                {
                    arrive3 = false;
                    yield return new WaitForSeconds(stayTime);
                    arrive4 = true;

                }
            }
            else if (toStart)
            {
                tempPosition = Vector3.MoveTowards(tempPosition, stopPosition2, speed * Time.deltaTime);
                transform.position = tempPosition;
                if (transform.position == stopPosition2)
                {
                    arrive3 = false;
                    yield return new WaitForSeconds(stayTime);
                    arrive2 = true;

                }
            }
        }

        if (arrive4)
        {
            if (toStart)
            {
                tempPosition = Vector3.MoveTowards(tempPosition, stopPosition3, speed * Time.deltaTime);
                transform.position = tempPosition;
                if (transform.position == stopPosition3)
                {
                    arrive4 = false;
                    yield return new WaitForSeconds(stayTime);
                    arrive3 = true;

                }
            }
        }
            
    }

    // 相对运动
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            other.transform.SetParent(null);
        }

    }

}

