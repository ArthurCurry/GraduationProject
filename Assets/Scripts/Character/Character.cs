using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character
{
    protected Rigidbody rigidbody;

    protected Transform self;
    protected Animator animator;

    protected Vector3 forwardDir;
    protected float moveSpeedXZ;
    protected float moveSpeedY;
    protected GameObject model;

    protected CharacterState m_state;//状态机

    public virtual void UpdateCharacter()
    {
        if(m_state==null)
            m_state=new PearlStateIdle(self,rigidbody,animator);
    }

    public virtual void InitCharacterData(Transform transform,Rigidbody rigidbody,Animator animator)
    {
        this.self=transform;
        this.rigidbody=rigidbody;
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
