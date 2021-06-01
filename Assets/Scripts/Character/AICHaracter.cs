using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState
{
    Search,
    Chase,
    Engage
}

public class AICHaracter : Character
{
    
    


    [Range(3,15)]
    [Tooltip("ai检测到玩家并走向玩家的距离")]
    public float detectRange;

    [Tooltip("ai交战距离")]
    public float engageRange;

    [Tooltip("ai脱战距离（至初始起点")]
    public float disengageRange;

    [Tooltip("追击玩家时的移动速度")]
    public float chaseSpeed;

    [Tooltip("检测时间")]
    [Range(0.1f,1.0f)]
    public float detectTime;

    // [Range(1,2)]
    // public float attackFrequency;

    public Dictionary<AIState,CharacterState> aistates = new Dictionary<AIState, CharacterState>();

    // Start is called before the first frame update
    void Start()
    {
        InitCharacter();
        m_state=new AISearchingState(this);
    }

    // Update is called once per frame
    void Update()
    {
        m_state.StateUpdate();
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
    private float detectTime;

    private Transform target;
    private float timer;

    private Collider[] detectBuffer=new Collider[]{};
    private float detectRange;

    public AIState statetype=AIState.Search;
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
        this.detectTime=ai.detectTime;
        this.detectRange=ai.detectRange;
        this.timer=0f;
        this.target=null;
        if(!ai.aistates.ContainsKey(this.statetype))
        {
            ai.aistates.Add(this.statetype,this);
        }
        // base.StateInit();
    }

    public override void StateUpdate()
    {
        Debug.Log(this.statetype);
        // Debug.Log(timer);
        base.StateUpdate();
        this.rigidbody.velocity=Vector3.zero;
        if(TimeUp())
        {
            DetectPlayerInRange();
        }
        TimerCount();
        Change();
    }

    public override void Change()
    {
        // base.Change();

        if(this.target!=null)
        {
            Debug.Log(target.name);
            this.ai.State=this.ai.aistates.ContainsKey(AIState.Chase)?this.ai.aistates[AIState.Chase]:new AIChaseState(this.ai,this.target);
            StateInit();
            this.ai.State.StateInit();
            return;
        }
        return;
        
    }

    public override void UpdateAnimationVariables()
    {
        // base.UpdateAnimationVariables();
    }

    private bool TimeUp()
    {
        if(timer>=detectTime)
        {
            timer=0;
            return true;
        }
        return false;
    }

    private void TimerCount()
    {
        timer+=Time.deltaTime;
    }

    private void DetectPlayerInRange()
    {
        // Debug.Log( );
        Debug.Log("detect");
        foreach(Collider collider in Physics.OverlapSphere(this.self.position,this.detectRange,1<<LayerMask.NameToLayer("Player")))
        {
            Debug.Log(collider.transform.name);
            if(collider.transform.tag.Equals("Player"))
            {
                this.target=collider.transform;
                return;
            }
        }
    }
}

public class AIChaseState:CharacterState
{

    private AICHaracter ai;
    private AIState stateType=AIState.Chase;

    private Transform target;

    private float chaseSpeed;

    private Vector3 fromAIToTarget;

    private float minumStepLength;

    private Vector3 originalPos;

    private bool onGround=false;

    private RaycastHit hitBeneath;

    private RaycastHit hitForward;

    private Transform terrainCurBeneath;

    public AIChaseState(AICHaracter ai,Transform target)
    {
        this.ai=ai;
        this.target=target;
        // StateInit();
    }

    public override void StateInit()
    {
        // base.StateInit();
        this.animator=ai.animator;
        this.self=ai.transform;
        this.rigidbody=ai.rb;
        this.chaseSpeed=ai.chaseSpeed;
        this.originalPos=this.self.position;
        if(!ai.aistates.ContainsKey(this.stateType))
            ai.aistates.Add(AIState.Chase,this);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        Debug.Log(this.stateType);

        fromAIToTarget=target.position-this.self.position;
        if(fromAIToTarget.magnitude<=this.ai.engageRange)
        {
            Change();
        }
        else if(fromAIToTarget.magnitude<this.ai.detectRange)
        {
            MoveToward(target.position);
        }
        else
        {
            if((originalPos-this.ai.transform.position).magnitude<0.1f)
            {
                ChangeToSearch();
            }
            else
            {
            MoveToward(originalPos);
            }
        }
    }

    public override void Change()
    {
        // base.Change();
        this.animator.SetBool("running",false);
        this.ai.State=this.ai.aistates.ContainsKey(AIState.Engage)?this.ai.aistates[AIState.Engage]:new AIAttackState(this.ai,this.target);
        this.ai.State.StateInit();
    }

    public void ChangeToSearch()
    {
        this.animator.SetBool("running",false);
        this.ai.State=this.ai.aistates.ContainsKey(AIState.Search)?this.ai.aistates[AIState.Search]:new AISearchingState(this.ai);

    }

    private void MoveToward(Vector3 position)
    {
        if(FallOnGround())
        {
            this.animator.SetBool("running",true);
            this.rigidbody.velocity=(position-this.ai.transform.position).normalized*this.chaseSpeed;
            this.ai.transform.LookAt(position,Vector3.up);
        }
        else
        {
            this.rigidbody.velocity.Set(this.rigidbody.velocity.x,this.rigidbody.velocity.y-ai.fallAcceleration,this.rigidbody.velocity.z);
        }
    }

    private bool FallOnGround()
    {
        Debug.DrawRay(this.self.position+this.ai.centorOffset,Vector3.down*this.ai.disToBottom,Color.red);
        if(Physics.Raycast(this.self.position+this.ai.centorOffset,Vector3.down,out hitBeneath,this.ai.disToBottom,LayerMask.GetMask("Terrain")))
        {
            terrainCurBeneath = hitBeneath.transform;
            try
            {
            this.self.SetParent(terrainCurBeneath.GetComponentInParent<AllMechanism>().transform);
            }
            catch{
                this.self.SetParent(null);
            }
            this.animator.SetBool("onground",true);
            return true;
        }
        this.animator.SetBool("onground",false);

        return false;
    }
}


public class AIAttackState:CharacterState
{

    private AICHaracter ai;
    private AIState state=AIState.Engage;
    private float timer;
    private float attackFrequency;
    private Transform target;
    private Vector3 targetDistance;
    public AIAttackState(AICHaracter ai,Transform target)
    {
        this.ai=ai;
        this.target=target;
    }

    public override void StateInit()
    {
        // base.StateInit();
        this.animator=ai.animator;
        this.self=ai.transform;
        this.rigidbody=ai.rb;
        this.attackFrequency=Random.Range(1,2);
        if(!ai.aistates.ContainsKey(this.state))
        {
            ai.aistates.Add(this.state,this);
        }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        Debug.Log(this.state);
        this.rigidbody.velocity=Vector3.zero;

        targetDistance=target.position-ai.transform.position;
        this.ai.transform.LookAt(target,Vector3.up);
        if(timer>=attackFrequency)
        {
            Attack();
            attackFrequency=Random.Range(1,2);
        }
        timer+=Time.deltaTime;
        Change();
    }

    public override void UpdateAnimationVariables()
    {
        // base.UpdateAnimationVariables();
    }

    public override void Change()
    {
        // base.Change();
        if(targetDistance.magnitude>=ai.disengageRange)
        {
            this.ai.State=this.ai.aistates[AIState.Search];
        }
    }

    private void Attack()
    {
        this.animator.SetTrigger("attack");
    }
}

