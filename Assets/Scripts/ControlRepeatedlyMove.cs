using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 来回移动平台的控制
/// </summary>
public class ControlRepeatedlyMove : AllMechanism
{

    [Tooltip("平台移动的结束位置")]
    public Vector3 stopPosition;
    [Tooltip("平台移动一次的时间")]
    public float moveTime = 1f;
    [Tooltip("平台到边界后的停留时间")]
    public float stayTime = 1f;
    [Tooltip("延迟执行")]
    public float delayTime = 1f;
    [Tooltip("对应机关")]
    public GameObject mechanism;

    private bool toStop = true;         // 是否朝结束位置移动
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
        PlatformMoveOn(on);
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
    IEnumerator PlatformMove( )
    {
        yield return new WaitForSeconds(delayTime);
        Vector3 tempPosition = transform.position;
        if (toStop&on)
        {
            tempPosition = Vector3.MoveTowards(tempPosition, stopPosition, speed * Time.deltaTime);
            transform.position = tempPosition;
            if (transform.position == stopPosition)
            {
                yield return new WaitForSeconds(stayTime);
                toStop = false;
            }
        }
        else if (!toStop&on)
        {
            tempPosition = Vector3.MoveTowards(tempPosition, startPosition, speed * Time.deltaTime);
            transform.position = tempPosition;
            if (transform.position == startPosition)
            {
                yield return new WaitForSeconds(stayTime);
                toStop = true;
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

