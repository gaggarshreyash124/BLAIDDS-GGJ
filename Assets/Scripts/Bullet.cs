using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 20f;
    void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.linearVelocityX = speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage(10);
            Destroy(this.gameObject);
        }
    }
}
