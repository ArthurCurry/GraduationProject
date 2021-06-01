using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMechanism : MonoBehaviour
{

    [Tooltip("机关是否执行")]
    public bool implement = false;
    private float CurrentTime = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;
        Debug.Log(CurrentTime);
    }


    public void ChangeStatus()
    {
        if (CurrentTime > 1)
        {
            implement = !implement;
            CurrentTime = 0;
        }
      
    }
}
