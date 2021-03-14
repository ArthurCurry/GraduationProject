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
    public float disToTop;
    public float disToBottom;
    public Vector3 centorOffset;
    public float initialJumpSpeed=5f;

    public float jumpHeight=2f;

    public float jumpTime;
    public float fallAcceleration=2f;



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



