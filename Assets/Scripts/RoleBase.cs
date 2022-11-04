using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RoleState
{
    idle,
    walk,
    wake,
    attack,
    stagger
}
public class RoleBase : MonoBehaviour
{
    public RoleState currentState;
    public Transform target;
    public FloatValue maxHealth;
    // public float roleHealth;
    public string roleName;
    public int roleDamage;
    public int moveSpeed;
    public float chaseRadius;
    public float attackRadius;
    public float preparingTime;
    public float attackDuration;
    public Vector3 roleDirection;
    private Rigidbody2D RoleRigidbody2D;
    public GameSignal playerHealthSingal;
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void Start()
    {

    }
    protected void Initialize(RoleID id)
    {
        RoleRigidbody2D = GetComponent<Rigidbody2D>();
        if(id == RoleID.log)
        {
            roleName = "log";
            roleDamage= 1;
            moveSpeed = 4;
            chaseRadius = 10;
            attackRadius = 1;
            preparingTime = 0.5f;
        }
        if(id == RoleID.zelda)
        {
            moveSpeed = 5;
            attackDuration=0.3f;
            roleDamage= 10;
        }
        Idling();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void RoleController()
    {
        roleDirection = Vector3.zero;
        roleDirection.x = Input.GetAxisRaw("Horizontal");
        roleDirection.y = Input.GetAxisRaw("Vertical");
        if(currentState == RoleState.walk || currentState == RoleState.idle)
        {
            RoleMoveControlled();
        }
    }
    private IEnumerator ExecuteSKill(SkillID skillID)
    {
        //following codes will be remodified
        GetComponent<Animator>().SetBool("Attack",true);
        currentState = RoleState.attack;
        yield return  null;
        GetComponent<Animator>().SetBool("Attack",false);
        currentState = RoleState.idle;
    }
    protected void AttackControlled(SkillID skillID)
    {
        StartCoroutine(ExecuteSKill(skillID));
    }
    protected void AttackSubjective(SkillID skillID)
    {
        StartCoroutine(ExecuteSKill(skillID));
    }
    protected void RoleMoveControlled()
    {
        GetComponent<Rigidbody2D>().MovePosition(transform.position + roleDirection* moveSpeed * Time.deltaTime);
        ControlledMovementAnimation();
    }
    protected void RoleMoveSubjective()
    {
        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed*Time.deltaTime);
        GetComponent<Rigidbody2D>().MovePosition(temp);
        ChangeAnim(temp - transform.position);

    }
    protected void Walk()
    {
        if(currentState == RoleState.idle)
        {
            StartCoroutine(RolePreparing());
        }
        else if(currentState == RoleState.walk)
        {
            StopCoroutine(RolePreparing());
            RoleMoveSubjective();
        }
    }
    protected IEnumerator RolePreparing()
    {
        
        GetComponent<Animator>().SetBool("Awake", true);
        ChangeState(RoleState.wake);
        yield return new WaitForSeconds(preparingTime);
        GetComponent<Animator>().SetBool("Walk", true);
        ChangeState(RoleState.walk);
    }
    protected void Idling()
    {
        if(currentState != RoleState.stagger && currentState != RoleState.idle)
            ChangeState(RoleState.idle);
            GetComponent<Animator>().SetBool("Awake",false);
            GetComponent<Animator>().SetBool("Walk", false);
            GetComponent<Animator>().SetBool("Attack", false);
    }
    protected void ControlledMovementAnimation()
    {
        if(roleDirection!= Vector3.zero)
        {
            GetComponent<Animator>().SetFloat("MoveX",roleDirection.x);
            GetComponent<Animator>().SetFloat("MoveY",roleDirection.y);
            GetComponent<Animator>().SetBool("Walk",true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Walk",false);
        }
    }
    protected void ChangeAnim(Vector3 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x>0)
            {
                GetComponent<Animator>().SetFloat("MoveX",1);
                GetComponent<Animator>().SetFloat("MoveY",0);
            }
            else if(direction.x<0)
            {
                GetComponent<Animator>().SetFloat("MoveX",-1);
                GetComponent<Animator>().SetFloat("MoveY",0);
            }
        }
        else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if(direction.y>0)
            {
                GetComponent<Animator>().SetFloat("MoveX",0);
                GetComponent<Animator>().SetFloat("MoveY",1);
            }
            else if(direction.y<0)
            {
                GetComponent<Animator>().SetFloat("MoveX",0);
                GetComponent<Animator>().SetFloat("MoveY",-1);
            }
        }
    }
    protected void ChangeState(RoleState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }
    public void Knock(float knockTime)
    {
        StartCoroutine(KnockCo(knockTime));
    }
    private IEnumerator KnockCo(float knockTime)
    {
        
        if(RoleRigidbody2D != null)
        {
            yield return new WaitForSeconds(knockTime);
            RoleRigidbody2D.velocity = Vector2.zero;
            currentState = RoleState.idle;
            RoleRigidbody2D.velocity = Vector2.zero;
        }
    }
}
