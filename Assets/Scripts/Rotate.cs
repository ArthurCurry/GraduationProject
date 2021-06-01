using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : AllMechanism
{
    [Tooltip("x旋转速度")]
    public float xSpeed = 25f;
    [Tooltip("y旋转速度")]
    public float ySpeed = 25f;
    [Tooltip("z旋转速度")]
    public float zSpeed = 25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, zSpeed * Time.deltaTime, Space.Self);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (other.transform.tag.Equals("Player"))
        {
            Debug.Log("player enter platform");
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
