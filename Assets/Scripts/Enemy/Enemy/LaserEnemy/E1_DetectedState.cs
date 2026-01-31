using UnityEngine;

public class E1_DetectedState: EnemyDetectState
{
    private E1_Base enemy;
    public E1_DetectedState(EnemyTemplete templete, StateMachine stateMachine, string animBoolName, EnemyData enemyData, E1_Base enemy) : base(templete, stateMachine, animBoolName, enemyData)
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
        if (Longrangeactiontime)
        {
            StateMachine.ChangeState(enemy.chargeState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            StateMachine.ChangeState(enemy.lookForPlayerState);
        }
    }
}
