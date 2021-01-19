using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPearl :Character
{
    private PearlStateIdle idleState;

    public CharacterPearl(Transform transform,Rigidbody rigidbody,Animator animator)
    {
        this.self=transform;
        this.rigidbody=rigidbody;
        this.animator=animator;
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

    public PearlStateIdle(Transform transform,Rigidbody rigidbody,Animator animator)
    {
        this.self=transform;
        this.rigidbody=rigidbody;
        this.animator=animator;
    }
    public override void StateInit()
    {
        base.StateInit();
    }

    public override void StateUpdate()
    {
        // base.StateUpdate();

    }

    public override void Change()
    {
        base.Change();
    }
}
