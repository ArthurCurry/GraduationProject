using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSystem :GameSystem
{

    private Character mainCharacter;
    private GameObject mcModel;
    public static Dictionary<int,Character> npcCharacters;

    public override void InitSystem()
    {

        // base.InitSystem();
        mcModel=Resources.Load<GameObject>(CharacterIndex.CharacterPearl);
        GameObject mcInstance=GameObject.Instantiate(mcModel,Vector3.zero,mcModel.transform.rotation);
        mainCharacter=new CharacterPearl(mcInstance.transform,mcInstance.GetComponent<Rigidbody>(),mcInstance.GetComponent<Animator>());
        
    }

    public override void OnSystemUpdate()
    {
        mainCharacter.UpdateCharacter();
    }

    public override void OnSystemStart()
    {
        base.OnSystemStart();
    }

    public override void OnSystemClose()
    {
        base.OnSystemClose();
    }
}
