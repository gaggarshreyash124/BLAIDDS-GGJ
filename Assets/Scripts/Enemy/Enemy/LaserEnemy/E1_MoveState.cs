using NUnit.Framework;
using UnityEngine;

public class E1_MoveState : EnemyMoveState
{
    private E1_Base enemy;

    public E1_MoveState(EnemyTemplete templete, StateMachine stateMachine, string animBoolName, EnemyData enemyData, E1_Base enemy) : base(templete, stateMachine, animBoolName, enemyData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMinAgroRange)
        {
            StateMachine.ChangeState(enemy.detectedState);
        }

        else if (isWallthere || !isLedgethere)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            StateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
