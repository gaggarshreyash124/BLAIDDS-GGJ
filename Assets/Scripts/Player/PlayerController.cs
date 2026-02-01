using System.Collections;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //components
    public Rigidbody2D rb;
    public InputHandler inputHandler;
    public RespawnHandler respawnHandler;
    public PlayerData playerdata;
    public Animator Anim;

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

    // Coyote jump
    bool CanUseCoyote => playerdata.cancoyote && !playerdata.isGrounded && Time.time < playerdata.coyotecheck + playerdata.CoyoteTime;

    //Maks Collected
    public Transform MaskPosition;
    public Transform Mask;
    public bool HasDJumpMask;
    public bool HasDashMask;

    bool GroundHit()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<InputHandler>();
    }

    private void Update()
    {  
        move();
        Flip();
        if(inputHandler.dashPressed && canDash && HasDashMask)
        {
            StartCoroutine(Dash());
        }
        Anim.SetBool("Run",inputHandler.movement.x !=0);
        Anim.SetBool("Inair",!playerdata.isGrounded);
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

        if (HasDJumpMask)
        {
            playerdata.maxJumps = 2;
        }
        else
        {
            playerdata.maxJumps = 1;
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
        if(playerdata.currentHealth <= 0)
        {
            Destroy(gameObject);
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