using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;
 


    protected void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
            target.GetComponent<Health>().TakeDamage(damage);
    }
}
