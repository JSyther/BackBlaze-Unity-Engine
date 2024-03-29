using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CannonTrap : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] cannBalls;
    private Animator animator;
    private float cooldownTimer;
    private bool isShooting;

    private void Attack()
    {
        cooldownTimer = 0f;
        isShooting = false;
        cannBalls[FindCannonBall()].transform.position = firePoint.position;
        cannBalls[FindCannonBall()].GetComponent<EnemyProjectiles>().ActivateProjectile();
    }

    private int FindCannonBall()
    {
        for (int i = 0; i < cannBalls.Length; i++)
        {
            if (!cannBalls[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        animator = GetComponent<Animator>();
        cooldownTimer += Time.deltaTime;

        if(!isShooting && cooldownTimer >= attackCooldown)
        {
            Attack();
            animator.SetTrigger("Shoot");
            isShooting = false;
        }
    }
}
