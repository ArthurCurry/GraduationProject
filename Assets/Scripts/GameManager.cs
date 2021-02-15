using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private Character initialCharacter;

    private CharacterSystem characterSystem;

    // Start is called before the first frame update
    void Start()
    {
        characterSystem=new CharacterSystem();
        characterSystem.InitSystem();
    }

    // Update is called once per frame
    void Update()
    {
        characterSystem.OnSystemUpdate();
    }
}
