using UnityEngine;

public class EnemyChargeState : State
{
    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool ChargeTimeOver;
    public EnemyChargeState(EnemyTemplete templete, StateMachine stateMachine, string animBoolName, EnemyData enemyData) : base(templete, stateMachine, animBoolName, enemyData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ChargeTimeOver = false;
        Templete.SetVelocity(enemyData.chargeSpeed);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= StartTime + enemyData.chargeTime)
        {
            ChargeTimeOver = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = Templete.CheckPlayerInMinAgroRange();
        isDetectingLedge = Templete.CheckLedge();
        isDetectingWall = Templete.CheckWall();
    }
}
