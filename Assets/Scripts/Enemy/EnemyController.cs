using UnityEngine;

public enum EnemyState
{
    Idle,
    Patrol,
    Detect,
    Chase,
    Search,
    Dead
}

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour, IDamageable
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform wallCheck;

    [Header("Movement")]
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 4f;
    [SerializeField] private float idleDuration = 2f;
    [SerializeField] private float patrolTime = 3f;

    [Header("Detection")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float minAgroDistance = 5f;
    [SerializeField] private float maxAgroDistance = 10f;

    [Header("Combat")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float attackPower = 10f;

    private EnemyState currentState;
    private float stateTimer;
    private bool facingRight = true;
    public float currentHealth;

    [Header("Contact Control")]
    [SerializeField] private float stopDistance = 0.8f;

    #region Unity Methods

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        SwitchState(EnemyState.Idle);
    }

    private void Update()
    {
        stateTimer += Time.deltaTime;

        switch (currentState)
        {
            case EnemyState.Idle:
                HandleIdle();
                break;

            case EnemyState.Patrol:
                HandlePatrol();
                break;

            case EnemyState.Detect:
                HandleDetect();
                break;

            case EnemyState.Chase:
                HandleChase();
                break;

            case EnemyState.Search:
                HandleSearch();
                break;
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    #endregion

    #region State Handlers

    private void HandleIdle()
    {
        rb.linearVelocityX = 0f;

        if (stateTimer >= idleDuration)
            SwitchState(EnemyState.Patrol);

        if (IsPlayerInMinAgroRange())
            SwitchState(EnemyState.Detect);
    }

    private void HandlePatrol()
    {
        Move(patrolSpeed);

        if (IsOnLedge() || IsTouchingWall() || stateTimer >= patrolTime)
        {
            Flip();
            SwitchState(EnemyState.Idle);
        }

        if (IsPlayerInMinAgroRange())
            SwitchState(EnemyState.Detect);
    }

    private void HandleDetect()
    {
        rb.linearVelocityX = 0f;

        if (stateTimer == 0)
            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);

        if (stateTimer >= 1f)
            SwitchState(EnemyState.Chase);
    }

    private void HandleChase()
    {
        if (!player) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // HARD STOP: prevents overlap
        if (distance <= stopDistance)
        {
            rb.linearVelocityX = 0f;
            return;
        }

        FacePlayer();
        Move(chaseSpeed);
    }


    private void HandleSearch()
    {
        rb.linearVelocityX = 0f;

        if (stateTimer >= 0.5f)
        {
            Flip();
            SwitchState(EnemyState.Patrol);
        }
    }
    

    #endregion

    #region Core Logic

    private void Move(float speed)
    {
        rb.linearVelocityX = speed * (facingRight ? 1 : -1);
    }

    private void ApplyMovement()
    {
        // Reserved for physics-safe future logic
    }

    private void FacePlayer()
    {
        if (!player) return;

        if (player.position.x > transform.position.x && !facingRight)
            Flip();
        else if (player.position.x < transform.position.x && facingRight)
            Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void SwitchState(EnemyState newState)
    {
        stateTimer = 0f;
        currentState = newState;
    }

    #endregion

    #region Checks

    private bool IsOnLedge()
    {
        return !Physics2D.OverlapCircle(ledgeCheck.position, 0.1f, groundLayer);
    }

    private bool IsTouchingWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.1f, groundLayer);
    }

    private bool IsPlayerInMinAgroRange()
    {
        return player && Vector2.Distance(transform.position, player.position) <= minAgroDistance;
    }

    private bool IsPlayerInMaxAgroRange()
    {
        return player && Vector2.Distance(transform.position, player.position) <= maxAgroDistance;
    }

    #endregion

    #region Damage

    public void TakeDamage(float damage)
    {
        if (currentState == EnemyState.Dead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentState = EnemyState.Dead;
            Destroy(gameObject);
        }
    }

    #endregion
}
