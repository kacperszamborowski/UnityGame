using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public SwordAttack swordAttack;

    private int halfScreenWidth;
    private int halfScreenHeight;

    public float moveSpeed = 1f;
    //public float collisionOffset = 0.01f;
    public ContactFilter2D movementFilter;
    bool canMove = true;
    bool canAttack = true;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    public Collider2D hitbox;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public Camera cam;
    public static bool dashing;
    public float dashDistance = 0.7f;
    public float dashDuration = 0.25f;
    public int playerLayer;
    public int enemyLayer;

    public ManaBar manaBar;
    public bool canRegenMana = true;
    public int maxMana = 20;
    public int mana = 20;
    public float manaRegenTime = 1.5f;
    public float manaRegenTimeElapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        halfScreenWidth = Screen.width / 2;
        halfScreenHeight = Screen.height / 2;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        dashing = false;

        playerLayer = LayerMask.NameToLayer("PlayerLayer");
        enemyLayer = LayerMask.NameToLayer("EnemyLayer");

        manaBar = GetComponentInChildren<ManaBar>();
        manaBar.SetMaxMana(maxMana);
        manaBar.SetMana(mana);
    }


    private void FixedUpdate()
    {
        if (!PauseMenu.isPaused)
        {
            if(rb != null)
            {
                if (canMove)
                {
                    if (movementInput != Vector2.zero)
                    {
                        bool success = TryMove(movementInput);

                        if (!success)
                        {
                            success = TryMove(new Vector2(movementInput.x, 0));

                            if (!success)
                            {
                                success = TryMove(new Vector2(0, movementInput.y));
                            }
                        }



                        if (movementInput.x < 0)
                        {
                            animator.SetBool("isMoving", true);
                            animator.SetBool("isMovingDown", false);
                            animator.SetBool("isMovingUp", false);
                            spriteRenderer.flipX = true;
                        }
                        else if (movementInput.x > 0)
                        {
                            animator.SetBool("isMoving", true);
                            animator.SetBool("isMovingDown", false);
                            animator.SetBool("isMovingUp", false);
                            spriteRenderer.flipX = false;
                        }
                        else if (movementInput.y < 0)
                        {
                            animator.SetBool("isMovingDown", true);
                            animator.SetBool("isMovingUp", false);
                            animator.SetBool("isMoving", false);
                        }
                        else if (movementInput.y > 0)
                        {
                            animator.SetBool("isMoving", false);
                            animator.SetBool("isMovingUp", true);
                            animator.SetBool("isMovingDown", false);
                        }

                    }
                    else
                    {
                        animator.SetBool("isMoving", false);
                        animator.SetBool("isMovingDown", false);
                        animator.SetBool("isMovingUp", false);
                    }
                }
            }
        }

        //mana regen
        if (canRegenMana)
        {
            if (mana < maxMana)
            {
                manaRegenTimeElapsed += Time.deltaTime;
                if (manaRegenTimeElapsed > manaRegenTime)
                {
                    mana += 2;
                    if (mana > maxMana)
                    {
                        mana = maxMana;
                    }
                    manaRegenTimeElapsed = 0f;
                }
            }
        }
        manaBar.SetMana(mana);

    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime /*+ collisionOffset*/);

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }


    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    public void LockMovement()
    {
        canMove = false;
        TurnOffHitbox();
    }

    public void UnlockMovement()
    {
        canMove = true;
        swordAttack.StopAttack();
    }

    public void LockAttack()
    {
        canAttack = false;
    }

    public void UnlockAttack()
    {
        canAttack = true;
    }

    void OnFire()
    {
        if (canAttack)
        {
            if (!PauseMenu.isPaused)
            {

                Vector3 mousePos = Input.mousePosition;

                float x = (mousePos.x - halfScreenWidth);
                float y = (mousePos.y - halfScreenHeight);

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x > 0.0f)
                    {
                        spriteRenderer.flipX = false;
                        animator.SetTrigger("player_attack");
                        swordAttack.AttackRight();
                    }
                    else
                    {
                        spriteRenderer.flipX = true;
                        animator.SetTrigger("player_attack");
                        swordAttack.AttackLeft();
                    }
                }
                else
                {
                    if (y > 0.0f)
                    {
                        animator.SetTrigger("player_attack_up");
                        swordAttack.AttackUp();
                    }
                    else
                    {
                        animator.SetTrigger("player_attack_down");
                        swordAttack.AttackDown();
                    }
                }
            }
        }
    }

    public void TurnOffHitbox()
    {
        hitbox.enabled = false;
    }

    public void TurnOnHitbox()
    {
        hitbox.enabled = true;
    }
    void OnRMB()
    {
        if (canAttack)
        {
            if (rb != null)
            {
                if (mana >= 10)
                {
                    mana -= 10;
                    LockMovement();
                    playerLayer = LayerMask.NameToLayer("PlayerLayer");
                    enemyLayer = LayerMask.NameToLayer("EnemyLayer");
                    Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
                    manaBar.SetMana(mana);
                    var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                    var direction = (mousePos - this.transform.position);

                    direction.z = 0;

                    if (direction.magnitude >= 0.1f)
                    {
                        this.StartCoroutine(this.DashRoutine(direction.normalized));
                    }
                }
            }
        }

    }
    IEnumerator DashRoutine(Vector3 direction)
    {
        if (this.dashDistance <= 0.001f)
            yield break;

        if (this.dashDuration <= 0.001f)
        {
            this.transform.position += direction * this.dashDistance;
            yield break;
        }

        dashing = true;
        var elapsed = 0f;
        var start = this.transform.position;
        var target = this.transform.position + this.dashDistance * direction;

        while (elapsed < this.dashDuration)
        {
            var iterTarget = Vector3.Lerp(start, target, elapsed / this.dashDuration);
            //this.transform.position = iterTarget;
            if(rb != null)
                rb.MovePosition(iterTarget);

            yield return null;
            elapsed += Time.deltaTime;
        }

        //this.transform.position = target;
        dashing = false;

        int playerLayer = LayerMask.NameToLayer("PlayerLayer");
        int enemyLayer = LayerMask.NameToLayer("EnemyLayer");
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);

        UnlockMovement();
    }

}