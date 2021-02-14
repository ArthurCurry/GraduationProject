using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem :GameSystem
{

    private delegate void CharaterAtk();
    private event CharaterAtk CharacterAtkHandler;

    public override void InitSystem()
    {
        base.InitSystem();
    }

    public override void OnSystemStart()
    {
        base.OnSystemStart();
    }

    public override void OnSystemUpdate()
    {
        base.OnSystemUpdate();
    }
    public override void OnSystemClose()
    {
        base.OnSystemClose();
    }

    
}
