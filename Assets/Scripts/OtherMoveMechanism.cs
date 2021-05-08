using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherMoveMechanism : MonoBehaviour
{

    [Tooltip("另一个开关")]
    public GameObject OtherMechanism;

    [Tooltip("机关是否执行")]
    public bool implement = false;


    // Start is called before the first frame update
    void Start()
    {
        implement = OtherMechanism.GetComponent<MoveMechanism>().implement;  // 接受控制机关的指令
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
