using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSystem :GameSystem
{

    private Character mainCharacter;
    private GameObject mcModel;
    public GameManager gm;
    public static Dictionary<int,Character> npcCharacters;

    public CharacterSystem()
    {

    }

    public CharacterSystem(GameManager gm)
    {
        this.gm=gm;
    }
    public override void InitSystem()
    {

        // base.InitSystem();
        mcModel=Resources.Load<GameObject>(CharacterIndex.DoubleSword);
        GameObject mcInstance=GameObject.Instantiate(mcModel,Vector3.zero,mcModel.transform.rotation);
        //mainCharacter=new CharacterPearl(mcInstance.transform,mcInstance.GetComponent<Rigidbody>(),mcInstance.GetComponent<Animator>());
        //mainCharacter=new DoubleSwordCharacter(mcInstance.transform,mcInstance.GetComponent<Rigidbody>(),mcInstance.GetComponent<Animator>(),this);
    }

    public override void OnSystemUpdate()
    {
        // mainCharacter.UpdateCharacter();
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
