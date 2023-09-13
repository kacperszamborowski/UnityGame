using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableEnemy : MonoBehaviour, IDamagable
{
    Animator animator;
    Rigidbody2D rb;

    public QuestScript quest;
    public HealthBar healthBar;
    public ExperienceManager exp;
    public GoldManager gold;

    public int level;

    private void Start()
    {
        _health = level * 5;
        exp = GameObject.FindGameObjectWithTag("Player").GetComponent<ExperienceManager>();
        gold = GameObject.FindGameObjectWithTag("Player").GetComponent<GoldManager>();
        quest = GameObject.FindGameObjectWithTag("Canvas").GetComponentInChildren<QuestScript>();
        animator = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
        healthBar.SetMaxHealth(_health);
        healthBar.SetHealth(_health);
    }

    public int Health
    {
        set
        {
            _health = value;
        }
        get
        {
            return _health;
        }

    }

    public int _health;
    private void Update()
    {
        if(quest == null)
        {
            quest = GameObject.FindGameObjectWithTag("Canvas").GetComponentInChildren<QuestScript>();
        }
        if (_health <= 0)
        {
            if (quest.sideQuestID == 2)
            {
                quest.sideQuestProgress += 1;
            }
            exp.expo += level * 5;
            gold.goldAmount += UnityEngine.Random.Range(1, level);
            Destroy(transform.parent.gameObject);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
            animator.SetTrigger("cos");

            rb.AddForce(knockback);

            Health -= damage;
            healthBar.SetHealth(_health);

    }

}
