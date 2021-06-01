using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWind : MonoBehaviour
{
    [Tooltip("X方向施加力")]
    public int Fe_x = 10;
    [Tooltip("Y方向施加力")]
    public int Fe_y = 10;
    [Tooltip("Z方向施加力")]
    public int Fe_z = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
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
