using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCombo : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audio_S;
    [SerializeField] private AudioClip[] audio_C;
    private Rigidbody2D rb;

    public int combo;
    public bool attackCombo;
    [SerializeField] private float rangeAttackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] thrownBlades;


    private float cooldownTimer = Mathf.Infinity;

    private PlayerMovement PM;


    void Start()
    {
        anim = GetComponent<Animator>();
        audio_S = GetComponent<AudioSource>();
        PM = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();   
    }

    public void Start_Combo()
    {
        attackCombo = false;
        if (combo < 3)
        {
            combo++;
        }
    }

    public void Finish_Animation()
    {
        attackCombo = false;
        combo = 0;
    }

    public void ComboLogic()
    {
        if(PM.OnWall() == false)
        {
            if (Input.GetMouseButtonDown(0) && !attackCombo)
            {
                attackCombo = true;
                anim.SetTrigger("" + combo);
                audio_S.clip = audio_C[combo];
                audio_S.Play();
                if (combo == 0)
                {
                    rb.transform.Translate(Vector2.zero);
                }
                if (combo == 1)
                {
                    rb.transform.Translate(Vector2.right * (4f) * Time.deltaTime);
                }
                if (combo == 2)
                {
                    rb.transform.Translate(Vector2.left * (2f) * Time.deltaTime);
                }
            }
        }
    }

    void Update()
    {
        ComboLogic();


        if (Input.GetMouseButton(1) && cooldownTimer > rangeAttackCooldown && PM.canAttack())
        {
            RangeAttack();
            cooldownTimer += Time.deltaTime;
        }

    }


    private void RangeAttack()
    {
        anim.SetTrigger("RangeAttack");
        cooldownTimer = 0;
        thrownBlades[FindThrownBlade()].transform.position = firePoint.position;
        thrownBlades[FindThrownBlade()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindThrownBlade()
    {
        for (int i = 0; i < thrownBlades.Length; i++)
        {
            if (!thrownBlades[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    
}

