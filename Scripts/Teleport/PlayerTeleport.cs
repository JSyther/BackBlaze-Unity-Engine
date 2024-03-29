using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{

    private GameObject currentTeleporter;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClip;

    private PlayerMovement PM;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PM = gameObject.GetComponent<PlayerMovement>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PM.IsGrounded())
        {
            if(currentTeleporter != null)
            {
                transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
                audioSource.clip = audioClip[0];
                audioSource.Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D pTarget)
    {
        if (pTarget.CompareTag("Teleporter"))
        {
            currentTeleporter = pTarget.gameObject;
        }
    }


    private void OnTriggerExit2D(Collider2D pTarget)
    {
        if (pTarget.CompareTag("Teleporter"))
        {
            if(pTarget.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }
}
