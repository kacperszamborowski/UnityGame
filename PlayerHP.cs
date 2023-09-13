using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour, IDamagablePlayer
{
    Animator animator;
    public Animator fade;
    Rigidbody2D rb;
    public ExperienceManager exp;
    public DeathMenu deathMenu;

    public HealthBar healthBar;

    public int maxHealth;

    public bool canTurnInvincible = true;
    public float invincibleTimeElapsed = 0f;
    public float invincibilityTime = 0.3f;


    public bool Invincible
    {
        set
        {
            _invincible = value;

            if(_invincible == true)
            {
                invincibleTimeElapsed = 0f;
            }
        }
        get
        {
            return _invincible;
        }
    }

    public bool _invincible = false;

    private void Start()
    {
        exp = GetComponent<ExperienceManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        GameObject gethp = GameObject.Find("PlayerHpBar");
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(_healthP);
    }

    public int HealthP
    {
        set
        {
            _healthP = value;

            if (_healthP <= 0)
            {
                animator.SetTrigger("player_death");
                fade.SetTrigger("fade_in");
                Destroy(rb);
                StartCoroutine(death());
                
            }
        }
        get
        {
            return _healthP;
        }

    }

    public int _healthP;

    public void OnHit2(int damage2, Vector2 knockback)
    {
        if (!Invincible)
        {
            rb.AddForce(knockback);

            HealthP -= damage2;
            healthBar.SetHealth(_healthP);
            if(canTurnInvincible)
            {
                Invincible = true;
            }
        }
        else if(PlayerController.dashing == true )
        {
            //cos tu mialo byc
        }
        else
        {
            rb.AddForce(knockback);
        }
    }

    private void FixedUpdate()
    {
        if(carDriveX.playerDashIntoCar == true)
        {
            animator.SetTrigger("player_death");
            fade.SetTrigger("fade_in");
            Destroy(rb);
            StartCoroutine(death());
        }
        if(PlayerController.dashing == true)
        {
            Invincible = true;
        }
        if(Invincible)
        {
            invincibleTimeElapsed += Time.deltaTime;

            if(invincibleTimeElapsed > invincibilityTime)
            {
                Invincible = false;
            }
        }
    }

    IEnumerator death()
    {
        yield return new WaitForSeconds(2f);

        deathMenu.DeadMenu();
    }

}
