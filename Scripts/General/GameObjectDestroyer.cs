using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDestroyer : MonoBehaviour
{
    public GameObject SpikeBall;
    public GameObject WoodSpike;
    private Rigidbody2D rb;
    private BoxCollider2D _trigger;

    private void Start()
    {
        gameObject.SetActive(true);
        SpikeBall = GameObject.Find("SpikeBall");
        rb = GetComponent<Rigidbody2D>();
        _trigger = GetComponent<BoxCollider2D>(); 
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            rb.velocity = new Vector2(5f, 3f);
            new Vector2(5f, 3f);
            StartCoroutine(DestroyObject(1));
            _trigger.enabled = false;
        }
    }


    IEnumerator DestroyObject(float destroytimer)
    {
        destroytimer = 1f;
        yield return new WaitForSeconds(destroytimer);
        Destroy(gameObject);

    }

}


