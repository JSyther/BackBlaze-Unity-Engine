using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodTraps : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject InactiveObjectTrapTrigger;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            InactiveObjectTrapTrigger.gameObject.SetActive(true);
            animator.SetTrigger("TrapStart");
            StartCoroutine(DisableObject());
            
        }
    }

    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(5f);
        InactiveObjectTrapTrigger.gameObject.SetActive(false);
        

    }
}
