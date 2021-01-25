using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameSystem 
{

    protected SystemState curState;


    public virtual void InitSystem()
    {
        
    }
    public virtual void OnSystemStart()
    {

    }
    public virtual void OnSystemUpdate()
    {

    }

    public virtual void OnSystemClose()
    {
        
    }
    
}

