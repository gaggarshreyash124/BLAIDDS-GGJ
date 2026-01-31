using UnityEngine;

public class EnemyTemplete : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }
    public EnemyData enemyData { get; set; }

    public int facingDirection = 1;
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }

    private Vector2 VelocityWorkspace;

    [SerializeField] Transform wallCheck;
    [SerializeField] Transform ledgeCheck;
    [SerializeField] Transform PlayerCheck;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        stateMachine = new StateMachine();
    }
    public virtual void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }
    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float Velocity)
    {
        VelocityWorkspace = new Vector2(facingDirection * Velocity, rb.linearVelocityY);
        rb.linearVelocity = VelocityWorkspace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, transform.right, enemyData.wallCheckDistance, enemyData.Ground);
    }
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, enemyData.ledgeCheckDistance, enemyData.Ground);
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);

    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(PlayerCheck.transform.position, transform.right, enemyData.minAgroDistance, enemyData.Player);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(PlayerCheck.transform.position, transform.right, enemyData.maxAgroDistance, enemyData.Player);
    }
}
