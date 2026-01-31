using UnityEngine;

public class E1_Base : EnemyTemplete
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_DetectedState detectedState { get; private set; }
    public E1_ChargeState chargeState { get; private set; }
    public E1_LookForPlayerState lookForPlayerState { get; private set; }

    [SerializeField] private EnemyData enemydata;

    public override void Start()
    {
        base.Start();

        idleState = new E1_IdleState(this, stateMachine, "idle", enemydata, this);
        moveState = new E1_MoveState(this, stateMachine, "move", enemydata, this);
        detectedState = new E1_DetectedState(this, stateMachine, "detected", enemydata, this);
        chargeState = new E1_ChargeState(this, stateMachine, "charge", enemydata, this);
        lookForPlayerState = new E1_LookForPlayerState(this, stateMachine, "lookForPlayer", enemydata, this);

        stateMachine.Initialize(idleState);
    }
}
