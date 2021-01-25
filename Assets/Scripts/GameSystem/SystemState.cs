using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SystemState
{
    
    protected SystemState curState;

    public virtual void StateUpdate()
    {

    }

    public virtual void StateChange()
    {
        
    }
}
