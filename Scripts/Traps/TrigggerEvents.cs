using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigggerEvents : MonoBehaviour
{
    private BoxCollider2D trigger;
    private Rigidbody2D rb;


    private float reactTime;
    private float time;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        trigger = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        if(time >= 2f)
        {
            time = 0f;
            return ;
        }
        RandomTime();
        

    }

    private void EventSpikeBall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        trigger.enabled = false;
    }




    void RandomTime()
    {
        reactTime = Random.Range(0f, 3f);
    }

    void OnTriggerEnter2D(Collider2D target)
    {

        
        if (target.tag == "Player" && time <= reactTime)
        {
            EventSpikeBall();
         
        }
    }

}
