using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Character:MonoBehaviour
{
    protected Rigidbody rb;

    protected Transform self;
    protected Animator animator;

    protected Vector3 forwardDir;
    protected float moveSpeedXZ;
    protected float moveSpeedY;
    protected GameObject model;
    public float initialJumpSpeed=5f;

    public float jumpHeight=2f;

    public float jumpTime;


    protected CharacterState m_state;//状态机

    public float Velocity;

    public virtual void UpdateCharacter()
    {
        if(m_state==null)
            m_state=new PearlStateIdle(self,rb,animator);
    }

    public virtual void InitCharacterData(Transform transform,Rigidbody rigidbody,Animator animator)
    {
        this.self=transform;
        this.rb=rigidbody;
        this.animator=animator;
        this.model=transform.gameObject;
    }

}

public enum CharacterMotionStatus
{
    Idle,
    Running,
    Walking,
    Jump,
    Normal,
    Combat
}

public abstract class CharacterState
{

    protected Rigidbody rigidbody;

    protected Transform self;
    protected Animator animator;

    protected CharacterState currentState;
    public virtual void StateInit()
    {

    }
    public virtual void StateUpdate()
    {

    }

    public virtual void Change()
    {

    }
}


public class DoubleSwordIdleState:CharacterState
{

    private Character selfCharacter;
    private Camera characterCam;
    private Vector3 curCamDir;
    private Vector3 curCharacterDir;
    private Vector3 inputDir;
    private float m_velocity=1.0f;
    private float xspeed;
    private float zspeed;
    private float yspeed;

    private float gravity=0.98f;
    private Vector3 rbVelocityLF;
    private float yPosLF;
    private bool isGrounded=true;

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

    public DoubleSwordIdleState(Transform transform,Rigidbody rigidbody,Animator animator,Character character)
    {
        this.self=transform;
        this.rigidbody=rigidbody;
        this.animator=animator;
        this.selfCharacter=character;
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
        inputDir=Vector3.zero;
        curCharacterDir=self.forward;
        curCamDir=characterCam.transform.forward;
        // this.velocity=selfCharacter.Velocity;
    }

    public override void StateUpdate()
    {
        xspeed=Input.GetAxisRaw("Horizontal");
        zspeed=Input.GetAxisRaw("Vertical");
        if(Input.GetKeyDown(KeyCode.Space)&&rbVelocityLF.y==0&&rigidbody.velocity.y==0)
        {
            yspeed=selfCharacter.initialJumpSpeed;
        }
        if(yPosLF==self.position.y)
        {
            yspeed=0f;
        }
        else
        {
            yspeed-=gravity*Time.deltaTime;
        }
        UpdateCharacterDirection();
        rigidbody.velocity=inputDir.normalized*selfCharacter.Velocity+yspeed*Vector3.up;        
        // rigidbody.velocity=new Vector3((xspeed>0?1:(xspeed==0?0:-1)),yspeed,(zspeed>0?1:(zspeed==0?0:-1))).normalized*selfCharacter.Velocity;
        // Debug.Log(xspeed+" "+zspeed+" "+selfCharacter.Velocity);
        rbVelocityLF=rigidbody.velocity;
        yPosLF=self.position.y;
    }


    private void UpdateCharacterDirection()
    {
        curCamDir=new Vector3(characterCam.transform.forward.x,0,characterCam.transform.forward.z);
        inputDir=new Vector3((xspeed>0?1:(xspeed==0?0:-1)),0,(zspeed>0?1:(zspeed==0?0:-1))).normalized;
        curCharacterDir=characterCam.transform.right*inputDir.x+characterCam.transform.forward*inputDir.z;
        curCharacterDir.y=0;
        inputDir=curCharacterDir;
        self.LookAt(self.transform.position+curCharacterDir);
    }
    public override void Change()
    {

    }
}
