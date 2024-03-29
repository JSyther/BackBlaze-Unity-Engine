using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class EnemyProjectiles : EnemyDamage
{


    private Rigidbody2D rigidBody;

    private float resetTime = 4f;
    private float lifetime;

    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rigidBody.AddForce(new Vector2(Random.Range(-10f, -15f), 1f));
        



        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }



}
