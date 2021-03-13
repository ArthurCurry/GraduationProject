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


