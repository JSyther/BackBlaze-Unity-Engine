using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Animator animator;

    private BoxCollider2D boxCollider;

    public AudioSource audio_s;
    public AudioClip[] audioClips;

    private float horizontal;
    private float wallJumpCooldown = 0f;



    [Range(0f, 10f)]
    public int moveSpeed;
    [Range(0f, 16f)]
    [SerializeField] private float jumpingPower;

    private bool isFacingRight = true;
    private bool isMoving;
    private bool isJump;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audio_s = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        //Running Logic 
        if (horizontal > 0f)
        {
            animator.SetBool("Run", true);
            isMoving = true;
        }
        else
        {
            animator.SetBool("Run", true);
            isMoving = true;
        }
        if (horizontal == 0f)
        {
            animator.SetBool("Run", false);
            isMoving = false;
        }




        //Jump Animation
        if (rb.velocity.y > 0 && !OnWall())
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
            isJump = true;
            if (gameObject.layer == 9)
            {
                animator.SetBool("Jump", false);
                animator.SetBool("Run", false);
                animator.SetBool("Climb", true);
                isJump = false;
            }
        }
        else if (rb.velocity.y == 0 || rb.velocity.y < 0 && OnWall())
        {
            animator.SetBool("Jump", false);
            isJump = false;
        }

        // Jump Logic
        if (wallJumpCooldown < 0.2f)
        {
            rb.velocity = new Vector2(horizontal * jumpingPower, rb.velocity.y);

            if (OnWall() && !IsGrounded())
            {
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.gravityScale = 4f;
            }
            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

        //Chanage Direction Function in use 
        FlipCharacter();
    }

    void FixedUpdate()
    {
        if (isMoving == true && !OnWall())
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }

        if (OnWall() && !IsGrounded())
        {
            animator.SetBool("Climb", true);
        }
        else if (!OnWall())
        {
            animator.SetBool("Climb", false);
        }
    }


    private void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

        }
        else if (OnWall() && !IsGrounded())
        {
            if (horizontal == 0f)
            {
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
            }
            else
                rb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6f);
            wallJumpCooldown = 0f;
        }

    }



    public bool IsGrounded()
    {
        RaycastHit2D raycashtHit = Physics2D.BoxCast(boxCollider.bounds.center,
            boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        return raycashtHit.collider != null;
    }
    public bool OnWall()
    {
        RaycastHit2D raycashtHit = Physics2D.BoxCast(boxCollider.bounds.center,
            boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);

        isJump = false;
        animator.SetBool("Jump", false);
        return raycashtHit.collider != null;
    }


    //Changing face direction of character's
    void FlipCharacter()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


    public bool canAttack()
    {
        return horizontal == 0 && IsGrounded() && !OnWall();
    }
}
