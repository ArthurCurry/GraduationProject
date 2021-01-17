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

    protected CharacterState state;//状态机



}

public enum CharacterMotionStatus
{
    Idle,
    Running,
    Walking,
    Jump
}

public abstract class CharacterState
{

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
