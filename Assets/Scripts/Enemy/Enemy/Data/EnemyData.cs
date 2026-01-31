using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    
    [Header("Idle State")]
    public float minIdleTime = 1f;
    public float maxIdleTime = 3f;

    [Header("Move State")]

    public float speed = 5f;
    
    [Header("Movement Checks")]
    public LayerMask Ground;
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float actionTime = 1.5f;

    [Header("Attack Checks")]
    public float minAgroDistance = 5f;
    public float maxAgroDistance = 15f;
    public LayerMask Player;

    [Header("Charge State")]
    public float chargeSpeed = 6f;
    public float chargeTime = 2f;

    [Header("Look For Player State")]
    public int EnemyTurns = 2;
    public float timeBetweenTurns = 0.75f;
}
