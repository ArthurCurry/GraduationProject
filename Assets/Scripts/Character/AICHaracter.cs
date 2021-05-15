using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICHaracter : Character
{
    
    [Range(3,15)]
    [Tooltip("ai检测到玩家并走向玩家的距离")]
    public float detectRange;

    [Tooltip("ai交战距离")]
    public float engageRange;

    [Tooltip("追击玩家时的移动速度")]
    
    public float chaseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitCharacter()
    {
        this.self=this.transform;
        this.rb=this.transform.GetComponent<Rigidbody>();
        this.animator=this.transform.GetComponent<Animator>();
        this.model=this.gameObject;
        
    }
}

public class AISearchingState:CharacterState
{
    private AICHaracter ai;
    public AISearchingState(AICHaracter ai)
    {
        this.ai=ai;
        StateInit();
    }
    public override void StateInit()
    {
        this.animator=ai.animator;
        this.self=ai.transform;
        this.rigidbody=ai.rb;
        // base.StateInit();
    }

    public override void StateUpdate()
    {
        // base.StateUpdate();

    }

    public override void Change()
    {
        // base.Change();
    }

    public override void UpdateAnimationVariables()
    {
        // base.UpdateAnimationVariables();
    }
}

