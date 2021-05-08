using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 来回移动平台的控制
/// </summary>
public class ControlMove : MonoBehaviour
{

    [Tooltip("平台移动的结束位置")]
    public Vector3 stopPosition;
    [Tooltip("平台移动一次的时间")]
    public float moveTime = 1f;
    [Tooltip("延迟执行")]
    public float delayTime = 1f;
    [Tooltip("对应机关")]
    public GameObject mechanism;

    private bool toStart = false;         // 是否朝开始位置移动
    private bool toStop = false;         // 是否朝结束位置移动
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
        on= mechanism.GetComponent<MoveMechanism>().implement;  // 接受控制机关的指令
        if (on)                    // 该部分是为了让他不自己乱动，控制机关给一次命令，就动一次
        {
            toStop = true;
            toStart = false;
        }
        else if(!on)
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
        StartCoroutine(PlatformMove(stopPosition));
    }

    /// <summary>
    /// 具体平台移动控制
    /// </summary>
    /// <param name="stopPosition">停止位置</param>
    /// <returns></returns>
    IEnumerator PlatformMove(Vector3 stopPosition)
    {
        yield return new WaitForSeconds(delayTime);
        Vector3 tempPosition = transform.position;
        if (toStop)
        {
            tempPosition = Vector3.MoveTowards(tempPosition, stopPosition, speed * Time.deltaTime);
            transform.position = tempPosition;

        }
         if (toStart)
        {
            tempPosition = Vector3.MoveTowards(tempPosition, startPosition, speed * Time.deltaTime);
            transform.position = tempPosition;

        }
    }

    // 相对运动
    void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }
    void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }

}

