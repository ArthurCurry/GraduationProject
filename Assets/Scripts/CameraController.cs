﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(-90,90)]
    public float verticalRotationDegreeDefault;
    public float verticalRotationDegreeOffset;

    [Range(0,360)]
    public float horizontalRotationDegreeDefault;
    public float horizontalRotationDegreeOffset;


    public float distanceToTarget;
    [Range(0f,5f)]
    public float horizontalSensitivity;

    [Range(0f,5f)]
    public float verticalSensitivity;
    public int pixelsPerDegree;

    public Transform targetObject;
    public Vector3 targetOffset=Vector3.zero;
    private Vector3 targetObjectPos;

    private Vector3 targetToCameraVector;
    private Vector3 defaultTargetToCameraVector;
    private Vector2 previousMousePos;

    private Vector3 horizontalVector;

    private Vector3 defaultHorizontalVector;
    private Vector3 verticalVector;
    private Vector2 curMousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(targetObject==null)
        {
            SwitchTarget(GameObject.FindWithTag("PlayerCenter").transform);
        }
        targetObjectPos=targetObject.position+targetOffset;

        CalcCameraPosition();

        AimOnObject(targetObjectPos);

        Debug.DrawRay(targetObjectPos,targetToCameraVector*distanceToTarget,Color.red);

    }

    public void SwitchTarget(Transform target)
    {
        if(target!=targetObject||targetObject==null)
        {
            targetObject=target;
        }
        Debug.Log("switch target");
        ResetStatus();

    }

    private void CalcCameraPosition()
    {
        curMousePos=Input.mousePosition;
        horizontalRotationDegreeOffset+=(curMousePos.x-previousMousePos.x)/pixelsPerDegree*horizontalSensitivity;
        // horizontalRotationDegreeOffset%=360;
        verticalRotationDegreeOffset+=(previousMousePos.y-curMousePos.y)/pixelsPerDegree*verticalSensitivity;
        // verticalRotationDegreeOffset=Mathf.Clamp(verticalRotationDegreeOffset,-90,90);
        horizontalVector=Quaternion.AngleAxis(horizontalRotationDegreeDefault+horizontalRotationDegreeOffset,targetObject.up)*defaultHorizontalVector.normalized;

        targetToCameraVector=Quaternion.AngleAxis(Mathf.Clamp(verticalRotationDegreeDefault+verticalRotationDegreeOffset,-80,80),targetObject.right)
        *(Quaternion.AngleAxis(horizontalRotationDegreeDefault+horizontalRotationDegreeOffset,targetObject.up)
        *defaultTargetToCameraVector);

        // targetToCameraVector=horizontalVector.normalized*distanceToTarget*Mathf.Cos(Mathf.Clamp(verticalRotationDegreeDefault+verticalRotationDegreeOffset,-90,90))+
        // Vector3.up*Mathf.Sin(Mathf.Clamp(verticalRotationDegreeDefault+verticalRotationDegreeOffset,-90,90))*distanceToTarget;


        transform.position=targetObjectPos+distanceToTarget*targetToCameraVector;

        previousMousePos=curMousePos;
        // Debug.Log(verticalRotationDegreeDefault+verticalRotationDegreeOffset+" "+horizontalRotationDegreeDefault+horizontalRotationDegreeOffset+" mouse:"+curMousePos);
        // Debug.Log(targetObject.right+" "+targetObject.up);
    }

    private void ResetStatus()
    {
        verticalRotationDegreeOffset=0;
        horizontalRotationDegreeOffset=0;
        defaultTargetToCameraVector=targetObject.forward*-1;
        curMousePos=Input.mousePosition;
        previousMousePos=curMousePos;

        defaultHorizontalVector=-targetObject.forward.normalized;
        verticalVector=-targetObject.forward.normalized;

        // horizontalVector=Quaternion.AngleAxis(Mathf.Clamp(horizontalRotationDegreeDefault,0,360),targetObject.up)*defaultHorizontalVector;
        // verticalVector=Quaternion.AngleAxis(Mathf.Clamp(verticalRotationDegreeDefault,-90,90),targetObject.right)*verticalVector;
        // targetToCameraVector=horizontalVector*distanceToTarget*Mathf.Cos(verticalRotationDegreeDefault)+Vector3.up*Mathf.Sin(verticalRotationDegreeDefault);

        targetToCameraVector=Quaternion.AngleAxis(Mathf.Clamp(verticalRotationDegreeDefault,-90,90),targetObject.right)
        *Quaternion.AngleAxis(Mathf.Clamp(horizontalRotationDegreeDefault,0,360),targetObject.up)
        *defaultTargetToCameraVector;

        this.transform.position=targetObject.position+targetOffset+distanceToTarget*targetToCameraVector;
    }

    // IEnumerator ZoomIn(Vector3 target,int zoomTimeMillesecond)
    // {
    //     transform.LookAt(target,Vector3.up);
    // }

    private void AimOnObject(Vector3 position)
    {
        transform.LookAt(position);
    }

    private void OnGUI() {
    }
}
