using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    //Charge State
    public float chargeSpeed = 6f;
    public float chargeTime = 2f;

    //Dead State
    public GameObject deathChunkParticle;
    public GameObject deathBloodParticle;

    //Entity
    public float maxHealth = 30f;
    
    public float damageHopSpeed = 3f;
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float groundCheckRadius = 0.3f;

    public float minAgroDistance = 3f;
    public float maxAgroDistance = 4f;

    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;

    public float closeRangeActionDistance = 1f;

    public GameObject hitParticle;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    //Idle State
    public float minIdleTime = 1f;
    public float maxIdleTime = 2f;

    //look for player state
    public int amountOfTurns = 2;
    public float timeBetweenTurns = 0.75f;

    //Move State
    public float movementSpeed = 3f;

    // PLayer Detected State
    public float longRangeActionTime = 1.5f;
}
