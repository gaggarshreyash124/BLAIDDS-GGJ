using System;
using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public enum States
{
    Idle,
    Move,
    Detect,
    Chase,
    Attack,
    Search,
    Dead
}

public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public States currentState;
    public Rigidbody2D Rb;
    public Transform LedgeCheck;
    public Transform WallCheck;
    public LayerMask GroundLayer;
    public float MinAgroDistance = 5f;
    public float MaxAgroDistance = 10f;
    public float MoveSpeed = 2f;
    public float PatrolTime = 3f;
    private bool movingRight = true;
    float Counter;
    public float ChaseSpeed = 4f;


    public bool IsOnLedge()
    {
        return !Physics2D.OverlapCircle(LedgeCheck.position, 0.1f, GroundLayer);
    }
    public bool IsTouchingWall()
    {
        return Physics2D.OverlapCircle(WallCheck.position, 0.1f, GroundLayer);
    }
    public bool IsPlayerInMinAgroRange()
    {
        float distance = Vector2.Distance(transform.position, Player.transform.position);
        return distance <= MinAgroDistance;
    }
    public bool IsPlayerInMaxAgroRange()
    {
        float distance = Vector2.Distance(transform.position, Player.transform.position);
        return distance <= MaxAgroDistance;
    }

    void Start()
    {
        SwitchState(States.Idle);
    }

    private void Update()
    {
        Counter += Time.deltaTime;
        switch (currentState)
        {
            case States.Move :
                Move();
                break;
            case States.Idle :
                Idle(2f);
                break;
            case States.Chase :
                Chase();
                break;
            case States.Search:
                // Search behavior to be implemented
                break;
        }

        if (IsPlayerInMinAgroRange())
        {
            currentState = States.Chase;
        }
        if (IsOnLedge() || IsTouchingWall() || Counter >= PatrolTime)
        {
            Flip();
            SwitchState(States.Idle);
        }
    }
    public void Move()
    {
        
        Rb.linearVelocityX = MoveSpeed * (movingRight ? 1 : -1);
    }

    public void Flip()
    {
        movingRight = !movingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void Idle(float duration)
    {
        Rb.linearVelocityX = 0;
        Invoke(nameof(IdleToMove), duration);
    }
    private void IdleToMove()
    {
        SwitchState(States.Move);
    }

    public void Chase()
    {
        if (IsPlayerInMaxAgroRange())
        {
            SwitchState(States.Search);
        }
        Rb.linearVelocityX = ChaseSpeed * (movingRight ? 1 : -1);

    }
    public void Search()
    {
        Rb.linearVelocityX = 0;
        Flip();
        Invoke(nameof(Flip), .5f);
        SwitchState(States.Move);
    }

    public void SwitchState(States newState)
    {
        Counter = 0;
        currentState = newState;
    }   

}
