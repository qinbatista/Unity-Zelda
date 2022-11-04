using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolePlayerZelda : RoleBase
{
    private Rigidbody2D thisRigidbody2D;
    
    private Animator thisAnimator;
    // Start is called before the first frame update
    void Start()
    {
        Initialize(RoleID.zelda);
        thisRigidbody2D = GetComponent<Rigidbody2D>();
        thisAnimator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        RoleController();
        if(Input.GetButtonDown("attack") && currentState!= RoleState.attack && currentState!=RoleState.stagger)
        {
            AttackControlled(SkillID.spiningAttack);
        }
    }
}
