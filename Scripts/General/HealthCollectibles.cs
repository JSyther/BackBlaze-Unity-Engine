using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectibles : MonoBehaviour
{
    [SerializeField] private float healthValue;
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            target.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}
