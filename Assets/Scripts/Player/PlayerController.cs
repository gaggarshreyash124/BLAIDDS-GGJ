using System.Collections;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour , IDamageable
{
    //components
    public Rigidbody2D rb;
    public InputHandler inputHandler;
    public RespawnHandler respawnHandler;
    public PlayerData playerdata;
    public Animator Anim;

    float KnockbackPower = 5f;
    //move
    public float facingDirection = 1f;
    public bool isFacingRight = true;

    //jump    
    public LayerMask groundLayer;
    public Transform groundCheck;

    //dash
    public bool canDash = true;
    private bool isDashing;

    //particles
    public ParticleSystem dustEffect;
    public BoxCollider2D boxc;

    //Attacking
    public BoxCollider2D attackBox;
    public LayerMask EnemyLayer;
    public bool Stunned = false;
    // Coyote jump
    bool CanUseCoyote => playerdata.cancoyote && !playerdata.isGrounded && Time.time < playerdata.coyotecheck + playerdata.CoyoteTime;
    bool GroundHit()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
    public bool IsAttackRange(out IDamageable Escript )
    {
        Escript = null;
        
        Collider2D Hit = Physics2D.OverlapBox(attackBox.bounds.center,attackBox.bounds.size,0,EnemyLayer);
        if (Hit == null)
        {
            return false;
        }

        Escript = Hit.GetComponent<IDamageable>();
        return Escript != null;
    }
    public bool IsTouchingEnemy(out Collider2D Hit)
    {
        Hit = null;
        Hit = Physics2D.OverlapBox(boxc.bounds.center, boxc.bounds.size, 0, EnemyLayer);
        if (Hit == null)
        {
            return false;
        }
        return Hit != null;
    }
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<InputHandler>();
    }
    private void Update()
    {  
        if (!Stunned)
        {
            move();
            Flip();

            if(inputHandler.dashPressed && canDash)
            {
                StartCoroutine(Dash());
            }

            Anim.SetBool("Run",inputHandler.movement.x !=0);
            Anim.SetBool("Inair",!playerdata.isGrounded);
            
            IDamageable damageable;

            if (inputHandler.attackPressed)
            {
                Anim.SetTrigger("Attack");
                inputHandler.attackPressed = false;
                if (IsAttackRange(out damageable))
                {
                    damageable.TakeDamage(playerdata.Attack);
                }
            }
            Collider2D Hit;
            if (IsTouchingEnemy(out Hit) && !Stunned)
            {
                Stunned = true;
                StartCoroutine(KnockbackCoroutine(Hit.GetComponent<Rigidbody2D>()));
            }
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();
        jump();

        if (!playerdata.isGrounded && GroundHit())
        {
            playerdata.isGrounded = true;
            playerdata.cancoyote = true;
        }
        else if (playerdata.isGrounded && !GroundHit())
        {
            playerdata.isGrounded = false;
            playerdata.coyotecheck = Time.time;
        }
    }

    public void move()
    {
        if(isDashing)
        {
            return;
        }

        rb.linearVelocityX = inputHandler.movement.x * playerdata.movespeed;
        
    }
    public void Flip()
    {
        if (isFacingRight && inputHandler.movement.x < 0f || !isFacingRight && inputHandler.movement.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            facingDirection *= -1f;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    public void jump()
    {
        if(isDashing)
        {
            return;
        }
        if(inputHandler.jumpPressed && playerdata.jumpsRemaining>0)
        {
            Anim.SetTrigger("Jump");
            inputHandler.jumpPressed = false;
            rb.linearVelocityY = playerdata.jumpforce;
            playerdata.jumpsRemaining--;
            dustEffect.Stop();
        }
    }
#region Attack, Damage
    public void TakeDamage(float damage)
    {
        playerdata.currentHealth -= damage;
        Stunned = true;
        if(playerdata.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        if (Stunned)
        {
            Stunned = false;
            rb.AddForce(new Vector2(-facingDirection * 5f, 5f), ForceMode2D.Impulse);
        }
    }

#endregion

#region Dash Mechanics
    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float gscale = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * playerdata.dashForce, 0);
        yield return new WaitForSeconds(playerdata.dashDuration);
        rb.gravityScale = gscale;
        isDashing = false;
        yield return new WaitForSeconds(playerdata.dashCooldown);
        canDash = true;
        dustEffect.Stop();
    }
#endregion

    IEnumerator KnockbackCoroutine(Rigidbody2D TargetBody)
    {
        // simple knockback + brief stun
        Stunned = true;
        Vector2 force = new Vector2((facingDirection * -1) * KnockbackPower,.2f);
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(force, ForceMode2D.Impulse);
        TargetBody?.AddForce(-force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        Stunned = false;
    }

#region Checks
    public void GroundCheck()
    {
        if(Physics2D.Raycast(groundCheck.transform.position, Vector2.down, 0.1f, groundLayer))
        {
            playerdata.jumpsRemaining = playerdata.maxJumps;
        }
    }
#endregion
    public void Dust()
    {
        dustEffect.Play();
    }
    public void Respawn()
    {
        transform.position = respawnHandler.CurrentRespawnCords;
    }
}
public interface IDamageable
{
    void TakeDamage(float damage);
}