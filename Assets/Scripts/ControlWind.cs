using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWind : MonoBehaviour
{
    [Tooltip("X方向施加力")]
    public int FirstFe_x = 10;
    [Tooltip("Y方向施加力")]
    public int FirstFe_y = 10;
    [Tooltip("Z方向施加力")]
    public int FirstFe_z = 10;
    [Tooltip("第二次X方向施加力")]
    public int SecondFe_x = 10;
    [Tooltip("第二次Y方向施加力")]
    public int SecondFe_y = 10;
    [Tooltip("第二次Z方向施加力")]
    public int SecondFe_z = 10;

    private int Fe_x;
    private int Fe_y;
    private int Fe_z;

    [Tooltip("对应机关")]
    public GameObject mechanism;
    internal bool on = true;           // 风场切换开关

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        on = mechanism.GetComponent<MoveMechanism>().implement;  // 接受控制机关的指令
        if (!on)                    // 该部分是为了让他不自己乱动，控制机关给一次命令，就动一次
        {
            Fe_x = FirstFe_x;
            Fe_y = FirstFe_y;
            Fe_z = FirstFe_z;
        }
        else if (on)
        {
            Fe_x = SecondFe_x;
            Fe_y = SecondFe_y;
            Fe_z = SecondFe_z;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            other.GetComponent<Rigidbody>().AddForce(Fe_x, Fe_y, Fe_z);
        }
        
     }

    private void OnTriggerExit(Collider other)
    {
        
    }

}
