using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;


    private BoxCollider2D boxCollider;
    private Animator animator;


    void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D> ();
    }


    void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        hit = true;
        boxCollider.enabled = false;
        animator.SetTrigger("Explode");

        if (target.tag == "Enemy")
            target.GetComponent<Health>().TakeDamage(1);
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
