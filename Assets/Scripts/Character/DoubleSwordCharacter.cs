using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSwordCharacter : Character
{
    private CharacterSystem cs;
    private void Start() {
        this.self=this.transform;
        this.rb=GetComponent<Rigidbody>();
        this.animator=GetComponent<Animator>();
        this.model=transform.gameObject;
        if(m_state==null)
        {
            m_state=new DoubleSwordIdleState(self,rb,animator,this);
        }
    }

   private void Update() {
       UpdateCharacter();
   }

    public override void UpdateCharacter()
    {
        m_state.StateUpdate();
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

    private Vector3 rbVelocityLF;
    private float yPosLF;
    private bool isGrounded=true;
    
    private int terrainLayer=LayerMask.GetMask("Terrain");
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
        yPosLF=float.NegativeInfinity;
        inputDir=Vector3.zero;
        curCharacterDir=self.forward;
        curCamDir=characterCam.transform.forward;
        // this.velocity=selfCharacter.Velocity;
    }

    public override void StateUpdate()
    {
        xspeed=Input.GetAxisRaw("Horizontal");
        zspeed=Input.GetAxisRaw("Vertical");
        // Debug.Log(FallOnGround());
        if(yspeed<=0)
            isGrounded=FallOnGround()?true:false;
        if(isGrounded)
        {
            yspeed=0;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                yspeed=selfCharacter.initialJumpSpeed;
                isGrounded=false;
            }
        }
        else
        {   
            // if(yPosLF==self.position.y)
            // {
            //     isGrounded=true;
            //     yspeed=0;
            // }
            // else
            // {
            //     yspeed-=selfCharacter.fallAcceleration*Time.deltaTime;
            // }
            if(yspeed>=0&&ImpactOnTop())
            {
                yspeed=0;
            }
            yspeed-=selfCharacter.fallAcceleration*Time.deltaTime;
            
        }
        UpdateCharacterDirection();
        rigidbody.velocity=inputDir.normalized*selfCharacter.Velocity+yspeed*Vector3.up;
        Debug.Log(rigidbody.velocity+" "+self.position.y+" "+yPosLF);        
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

    private bool FallOnGround()
    {
        RaycastHit raycastHit=new RaycastHit();
        Debug.DrawRay(self.position+selfCharacter.centorOffset,Vector3.down*selfCharacter.disToBottom,Color.red);
        if(Physics.Raycast(self.position+selfCharacter.centorOffset,Vector3.down,out raycastHit,selfCharacter.disToBottom,terrainLayer))
        {
            
            return true;
        }
        Debug.Log(raycastHit.transform==null);
        return false;
    }

    private bool ImpactOnTop()
    {
        if(Physics.Raycast(self.position+selfCharacter.centorOffset,Vector3.up,selfCharacter.disToTop,terrainLayer))
            return true;
        return false;
    }
    public override void Change()
    {

    }
}
