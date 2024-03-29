using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WpodSpikeOff : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

   

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            boxCollider.enabled = false;
        }
    }


}
