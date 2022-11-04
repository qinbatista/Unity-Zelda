using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D target = other.GetComponent<Rigidbody2D>();
        if(target.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            target.GetComponent<Potory>().Smash();
        }

        if(target.gameObject.CompareTag("enemy")|| target.gameObject.CompareTag("Player"))
        {
            
            
            if(target != null)
            {
                // target.GetComponent<Enemy>().currentState = EnemyState.stagger;
                target.isKinematic = false;
                Vector2 difference = target.transform.position - transform.position;
                difference = difference.normalized * thrust;
                target.AddForce(difference, ForceMode2D.Impulse);

                if(target.gameObject.CompareTag("enemy"))
                {
                    target.GetComponent<RoleBase>().currentState = RoleState.stagger;
                    target.GetComponent<RoleBase>().maxHealth.RuntimeValue -= transform.parent.GetComponent<RoleBase>().roleDamage;
                    if(target.GetComponent<RoleBase>().maxHealth.RuntimeValue<0)
                    {
                        target.gameObject.SetActive(false);
                        Debug.Log("health="+target.GetComponent<RoleBase>().maxHealth.RuntimeValue);
                    }
                    else
                    {
                        Debug.Log("health="+target.GetComponent<RoleBase>().maxHealth.RuntimeValue);
                    }
                    Knock(target, knockTime);
                }
                if(target.gameObject.CompareTag("Player"))
                {
                    target.GetComponent<RoleBase>().currentState = RoleState.stagger;
                    target.GetComponent<RoleBase>().maxHealth.RuntimeValue -= transform.GetComponent<RoleBase>().roleDamage;
                    target.GetComponent<RoleBase>().Knock(knockTime);
                    if(target.GetComponent<RoleBase>().maxHealth.RuntimeValue >= 0)
                    {
                        // Debug.Log("target.GetComponent<RoleBase>().playerHealthSingal.Raise()");
                        target.GetComponent<RoleBase>().playerHealthSingal.Raise();
                    }
                }
                // StartCoroutine(KnockCo(target));
            }
        }
    }
    private IEnumerator KnockCo(Rigidbody2D Role)
    {
        if(Role != null)
        {
            Debug.Log("enemy="+Role);
            yield return new WaitForSeconds(knockTime);
            Role.velocity = Vector2.zero;
            Role.isKinematic = true;
            Role.GetComponent<RoleBase>().currentState = RoleState.idle;
        }

    }


    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
    }
    private IEnumerator KnockCo(Rigidbody2D role, float knockTime)
    {
        if(role != null)
        {
            yield return new WaitForSeconds(knockTime);
            role.velocity = Vector2.zero;
            role.isKinematic = true;
            role.GetComponent<RoleBase>().currentState = RoleState.idle;
            role.velocity = Vector2.zero;
        }
    }
}
