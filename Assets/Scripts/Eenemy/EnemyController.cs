using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float EnemyMoveSpeed;
    public float ChargeSpeed;
    public float EnemyHealth;
    public int EnemyDamage;
    [SerializeField]Transform GroundCheck;
    [SerializeField] Transform WallCheck;
    public LayerMask GroundLayer;
    public LayerMask whatIsPlayer;
    float Counter = 0;
    Rigidbody2D Rb;
    [SerializeField] float PatrolTime;
    [SerializeField] float IdleTime;
    [SerializeField] float StunTime = .2f;

    float minAgroDistance = 3f;
    float maxAgroDistance = 7f;

    //State Bools
    bool Patrol;
    public bool Charge;
    bool Detected;
    bool Hit;
    bool Search;


    public int States;
    public bool IsNotGround()
    {
        return !Physics2D.Raycast(GroundCheck.position, Vector2.down,.1f,GroundLayer);
    }

    bool IsWall()
    {
        return Physics2D.Raycast(WallCheck.position,Vector2.right * transform.localScale.x,.1f,GroundLayer);
    }
    
     public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(transform.position, transform.right, minAgroDistance, whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(transform.position, transform.right, maxAgroDistance, whatIsPlayer);
    }

    void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Counter += Time.deltaTime;

        switch (States)
        {
            case 0 :
                bool isMoving = true;
                Rb.linearVelocityX = isMoving ? EnemyMoveSpeed * transform.localScale.x : 0;

                if (isMoving)
                {
                    if (IsNotGround()  || Counter >= PatrolTime || IsWall() )
                    {
                        Flip();
                        isMoving = false;
                        Counter = 0;
                    }
                }
                else if (Counter >= IdleTime && !isMoving)
                {
                    isMoving = true;
                    Counter = 0;
                }
                break;

            case 1 :
                StartCoroutine(ChargePlayer());
                break;

            case 2 :
                Rb.linearVelocityX = 0;
                break;
            
            case 3 :
                StartCoroutine(StunR());
                break;
            
            case 4 :
                StartCoroutine(SearchPlayer());
                break;
        }
        if (Patrol)
        {
            States = 0;
        }
        else if (CheckPlayerInMinAgroRange())
        {
            StartCoroutine(DetectedPlayer());
        }
        else if (!CheckPlayerInMaxAgroRange())
        {
            States = 4;
        }
    }
    
    IEnumerator StunR()
    {
        Rb.linearVelocityX = 0.2f;
        yield return new WaitForSeconds(StunTime);
    }


    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    IEnumerator ChargePlayer()
    {
        Rb.linearVelocityX = ChargeSpeed;
        yield return new WaitForSeconds(.5f);
    }
    IEnumerator SearchPlayer()
    {
        yield return new WaitForSeconds(.2f);
        Flip();
        yield return new WaitForSeconds(.2f);
        Flip();
    }

    IEnumerator DetectedPlayer()
    {
        States = 2;
        yield return new WaitForSeconds(.5f);
        States = 1;
    }

    void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,new Vector3(minAgroDistance + transform.position.x,transform.position.y + .1f,transform.position.z));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,new Vector3(maxAgroDistance + transform.position.x,transform.position.y,transform.position.z));

    }


}
