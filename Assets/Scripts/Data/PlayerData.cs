using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    [Space]
    public float movespeed = 5f;

    [Header("Jumping Mechanics")]
    [Space]
    public float jumpforce = 10f;
    public int maxJumps =2;
    public int jumpsRemaining;
    public bool isGrounded;
    //Coyote
    public float coyotecheck;
    public bool cancoyote;
    public float CoyoteTime = 0.15f;

    [Header("Dash Mechanics")]
    [Space]
    public float dashForce = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    [Header("Health and Attack")]
    [Space]
    public float Attack = 10f;
    public int maxHealth =10;
    public float currentHealth;

    public bool Dead;
}
