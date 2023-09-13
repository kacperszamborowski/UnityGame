using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carDriveX : MonoBehaviour
{
    public float driveDistance = 100f;
    public float driveDuration = 4.5f;
    public Vector2 start;
    public Vector2 end;

    public static bool playerDashIntoCar;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    //public Sprite upSprite;
    //public Sprite downSprite;
    void Start()
    {
        playerDashIntoCar = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //start i end z car spawnera
        var direction = (end - new Vector2(this.transform.position.x, this.transform.position.y));

        if (direction.x > 0 && direction.y == 0)
        {
            spriteRenderer.flipX = true;
            driveDuration = Mathf.Abs(direction.x) / 2;
        }
        else
        {
            driveDuration = Mathf.Abs(direction.x) / 2;

        }
        if (direction.x == 0 && direction.y > 0)
        {
            driveDuration = Mathf.Abs(direction.y) / 2;
        }
        if (direction.x == 0 && direction.y < 0)
        {
            driveDuration = Mathf.Abs(direction.y) / 2;
        }

        // dodatkowe zabezpieczenie
        if (direction.magnitude >= 0.1f)
        {
            // Don't exceed the target, you might not want this
            this.StartCoroutine(this.CarMove(direction.normalized));
        }
    }

    IEnumerator CarMove(Vector3 direction)
    {
        //z  
        if (this.driveDistance <= 0.001f)
            yield break;

        if (this.driveDuration <= 0.001f)
        {
            this.transform.position += direction * this.driveDistance;
            yield break;
        }

        
        var elapsed = 0f;

        while (elapsed < this.driveDuration)
        {
            var iterTarget = Vector3.Lerp(start, end, elapsed / this.driveDuration);
            rb.MovePosition(iterTarget);

            yield return null;
            elapsed += Time.deltaTime;
        }
    }

    public void OnCollisionStay2D(Collision2D col)
    {
        Rigidbody2D targetBody = col.collider.GetComponent<Rigidbody2D>();
        if (targetBody != null)
        {
            if (col.collider.tag == "Player")
            {
                if(PlayerController.dashing == true)
                {
                    playerDashIntoCar = true;
                }
                IDamagablePlayer damagableObject = col.collider.GetComponent<IDamagablePlayer>();
                if (damagableObject != null)
                { 
                    damagableObject.OnHit2(9999, Vector2.zero);
                }
            }
        }
        if (col.collider.tag == "EntityHitbox")
        {
            IDamagable damagableObject = col.collider.GetComponent<IDamagable>();
            if (damagableObject != null)
            {
                damagableObject.OnHit(9999, Vector2.zero);
            }
        }
    }
}
