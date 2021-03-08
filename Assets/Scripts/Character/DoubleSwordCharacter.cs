using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSwordCharacter : Character
{
    public DoubleSwordCharacter(Transform transform,Rigidbody rigidbody,Animator animator)
    {
        this.self=transform;
        this.rigidbody=rigidbody;
        this.animator=animator;
        this.model=transform.gameObject;
        if(m_state==null)
            m_state=new DoubleSwordIdleState(self,rigidbody,animator);
    }

    public override void UpdateCharacter()
    {
        m_state.StateUpdate();
    }
}

public class DoubleSwordIdleState:CharacterState
{
    private Camera characterCam;
    private Vector3 curCamDir;
    private float m_velocity=1.0f;
    private float xspeed;
    private float zspeed;
    private float yspeed;

    private float gravity=0.98f;
    private Vector3 rbVelocityLF;
    private float yPosLF;

    private float initialJumpSpeed=5f;
    public float velocity
    {

        get{
            return m_velocity;
        }
        set{
            m_velocity=value;
        }

    }


    public DoubleSwordIdleState(Transform transform,Rigidbody rigidbody,Animator animator)
    {
        this.self=transform;
        this.rigidbody=rigidbody;
        this.animator=animator;
        if(characterCam==null)
            characterCam=Camera.main;
        StateInit();
    }

    public override void StateInit()
    {
        curCamDir=Camera.main.transform.forward;
        curCamDir.z=0;
        rbVelocityLF=rigidbody.velocity;
        yPosLF=self.position.y;
    }

    public override void StateUpdate()
    {
        xspeed=Input.GetAxis("Horizontal");
        zspeed=Input.GetAxis("Vertical");
        if(Input.GetKeyDown(KeyCode.Space)&&rbVelocityLF.y==0&&rigidbody.velocity.y==0)
        {
            yspeed=initialJumpSpeed;
            rigidbody.velocity.Set(rigidbody.velocity.x,yspeed,rigidbody.velocity.z);
        }
        if(yPosLF==self.position.y)
        {
            yspeed=0f;
        }
        else
        {
            yspeed-=gravity*Time.deltaTime;
        }
        rigidbody.velocity.Set((xspeed>0?1:(xspeed==0?0:-1))*velocity,yspeed,(zspeed>0?1:(zspeed==0?0:-1))*velocity);
        Debug.Log(rigidbody.velocity);
        rbVelocityLF=rigidbody.velocity;
        yPosLF=self.position.y;
    }

    public override void Change()
    {

    }
}
