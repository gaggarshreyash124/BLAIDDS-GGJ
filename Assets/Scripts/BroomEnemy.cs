using System;
using UnityEngine;

public class BroomEnemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject Player;
    public float Speed;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,Player.transform.position, Speed * Time.deltaTime);
    }
}
