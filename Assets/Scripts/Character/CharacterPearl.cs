using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPearl :Character
{
    private PearlStateIdle idleState;
    public CharacterPearl(Transform transform,Rigidbody rigidbody,Animator animator)
    {
        this.self=transform;
        this.rb=GetComponent<Rigidbody>();
        this.animator=this.GetComponent<Animator>();
        this.model=transform.gameObject;
        if(idleState==null)
            idleState=new PearlStateIdle(self,rigidbody,animator);
        
    }

    
    public override void UpdateCharacter()
    {
        // base.UpdateCharacter();
        idleState.StateUpdate();
    }
}

public class PearlStateIdle:CharacterState
{

    private Vector3 direction;
    public PearlStateIdle(Transform transform,Rigidbody rigidbody,Animator animator)
    {
        this.self=transform;
        this.rigidbody=rigidbody;
        this.animator=animator;
        direction=Vector3.zero;
    }
    public override void StateInit()
    {
        base.StateInit();
    }

    public override void StateUpdate()
    {
        // base.StateUpdate();
        if(Input.GetKeyDown(KeyCode.A))
            animator.SetTrigger("left");
        if(Input.GetKeyDown(KeyCode.D))
            animator.SetTrigger("right");
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("jump");
            return;
        }
        direction.x=Input.GetAxis("Horizontal");
        direction.z=Input.GetAxis("Vertical");
        
        animator.SetInteger("xspeed",direction.x==0?0:(direction.x>0?1:-1));
        animator.SetInteger("zspeed",direction.z==0?0:(direction.z>0?1:-1));


    }

    public override void Change()
    {
        base.Change();
    }
}
