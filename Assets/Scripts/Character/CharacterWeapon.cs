using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    // Start is called before the first frame update

    public int damage=50;

    public Character character;

    private Animator characterAnim;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitWeaponStatus()
    {
        character=transform.GetComponentInParent<Character>();
        characterAnim=transform.GetComponentInParent<Animator>();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag.Equals("MoveMechanism"))
        {
            MoveMechanism mm=other.transform.GetComponent<MoveMechanism>();
            mm.implement=!mm.implement;
        }
    }
}
