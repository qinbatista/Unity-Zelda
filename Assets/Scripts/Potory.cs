using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potory : MonoBehaviour
{
    private Animator thisAnim;
    // Start is called before the first frame update
    void Start()
    {
        thisAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Smash()
    {
        thisAnim.SetBool("smash",true);
        StartCoroutine(breakCo());
    }
    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
    }
}
