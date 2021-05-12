using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    // Start is called before the first frame update

    public int damage=50;

    public Character character;

    private Animator characterAnim;

    private Animator characterCurAnimState;

    private BoxCollider col;

    void Start()
    {
        InitWeaponStatus();
    }

    // Update is called once per frame
    void Update()
    {
        if(col.enabled==false&&characterAnim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            col.enabled=true;
        }
        if(!characterAnim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            col.enabled=false;
        }
    }

    public void InitWeaponStatus()
    {
        col=this.GetComponent<BoxCollider>();
        col.enabled=false;
        characterAnim=transform.GetComponentInParent<Animator>();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name+" "+other.transform.tag);
        if(other.transform.tag.Equals("MoveMechanism"))
        {
            MoveMechanism mm=other.transform.parent.GetComponent<MoveMechanism>();
            mm.implement=!mm.implement;
        }
        else if(other.transform.tag.Equals("Player")||other.transform.tag.Equals("AICharacter"))
        {
            Character character=other.transform.GetComponent<Character>();
            character.hp-=this.damage;
            character.hp=character.hp>=0?character.hp:0;
        }
    }
}
