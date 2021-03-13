using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private Character initialCharacter;
    public float playerCharacterSpeed=1f;

    private CharacterSystem characterSystem;

    // Start is called before the first frame update
    void Start()
    {
        characterSystem=new CharacterSystem(this);
        characterSystem.InitSystem();
    }

    // Update is called once per frame
    void Update()
    {
        characterSystem.OnSystemUpdate();
    }
}
