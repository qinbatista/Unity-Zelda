using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleMonsterLog : RoleBase
{
    // Start is called before the first frame update
    void Start()
    {
        Initialize(RoleID.log);
        target = GameObject.FindWithTag("Player").transform;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }
    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position)<= chaseRadius && Vector3.Distance(target.position, transform.position)> attackRadius)
        {
            Walk();
        }
        else if(Vector3.Distance(target.position, transform.position)<= attackRadius)
        {
            AttackSubjective(SkillID.spiningAttack);
        }
        else if(Vector3.Distance(target.position, transform.position)> chaseRadius && Vector3.Distance(target.position, transform.position)> attackRadius)
        {
            Idling();
        }
    }
}
