using UnityEngine;

public class EnemyMoveState : State
{
    protected bool isWallthere;
    protected bool isLedgethere;
    protected bool isPlayerInMinAgroRange;

    public EnemyMoveState(EnemyTemplete Templete, StateMachine stateMachine, string animboolname, EnemyData enemyData) : base(Templete, stateMachine, animboolname, enemyData)
    {


    }
    public override void Enter()
    {
        base.Enter();
        Templete.SetVelocity(enemyData.speed);

    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        isPlayerInMinAgroRange = Templete.CheckPlayerInMinAgroRange();
        isWallthere = Templete.CheckWall();
        isLedgethere = Templete.CheckLedge();

    }
}
