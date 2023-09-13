using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public const float knockbackForce = 100f;
    public int damage;

    public ExperienceManager stats;

    BoxCollider2D swordCollider;
    private void Start()
    {
        stats = GetComponentInParent<ExperienceManager>();
        swordCollider = GetComponent<BoxCollider2D>();
        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "EntityHitbox")
        {
            IDamagable damagableObject = col.GetComponent<IDamagable>();
            if (damagableObject != null)
            {
                Vector3 parentPosition = transform.parent.position;

                Vector2 direction = (col.transform.position - parentPosition).normalized;
                Vector2 knockback = direction * knockbackForce;

                damage = stats.level * 5;
                if(PlayerController.dashing == false)
                    damagableObject.OnHit(damage, knockback);
                else
                    damagableObject.OnHit(damage * 2, Vector2.zero);
            }
        }
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(0.12f, 0f);
        swordCollider.size = new Vector2(0.18f, 0.23f);
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(-0.12f, 0f);
        swordCollider.size = new Vector2(0.18f, 0.23f);
    }

    public void AttackUp()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(0f, 0.065f);
        swordCollider.size = new Vector2(0.23f, 0.15f);
    }

    public void AttackDown()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(0f, -0.102f);
        swordCollider.size = new Vector2(0.23f, 0.13f);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
        transform.localPosition = new Vector2(100f, 100f);
    }
}
