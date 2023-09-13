using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    public CapsuleCollider2D hitbox;
    public GameObject chaseTarget;
    public float speed = 0.9f;
    public float distance;
    public int currentFollowerID;



    void Start()
    {
        hitbox = GetComponent<CapsuleCollider2D>();
        chaseTarget = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, chaseTarget.transform.position);
        Vector2 chaseDirection = chaseTarget.transform.position - transform.position;
        if (FollowerSkill.canFollowerMove == true)
        {
            if (distance > 0.3f && distance <= 2.5f)
            {
                if (chaseDirection.x < 0)
                {
                    animator.SetTrigger("gasniok_walk");
                    spriteRenderer.flipX = true;
                }
                else if(chaseDirection.x > 0)
                {
                    animator.SetTrigger("gasniok_walk");
                    spriteRenderer.flipX = false;
                }
                speed = 0.85f;
                transform.position = Vector2.MoveTowards(this.transform.position, chaseTarget.transform.position, speed * Time.deltaTime);
                hitbox.enabled = true;
            }
            else if (distance > 2.5f)
            {
                CatchUp();
            }
            else if (distance <= 0.3f)
            {
                hitbox.enabled = false;
                animator.SetTrigger("gasniok_idle");
            }
        }
    }

    void CatchUp()
    {
        var elapsed = 0f;
        if (elapsed < 1f)
        {
            elapsed += Time.deltaTime;
            speed = 3f;
            Vector2 chaseDirection = chaseTarget.transform.position - transform.position;

            transform.position = Vector2.MoveTowards(this.transform.position, chaseTarget.transform.position, speed * Time.deltaTime);
            hitbox.enabled = false;
        }
    }

}
