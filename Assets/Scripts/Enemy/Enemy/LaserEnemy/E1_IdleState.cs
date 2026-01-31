using UnityEngine;

public class E1_IdleState : EnemyIdleState
{
    private E1_Base enemy;
    public E1_IdleState(EnemyTemplete templete, StateMachine stateMachine, string animBoolName, EnemyData enemyData, E1_Base enemy) : base(templete, stateMachine, animBoolName, enemyData)
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
        
        else if (isidleover)
        {
            StateMachine.ChangeState(enemy.moveState);
        }
    }
}
