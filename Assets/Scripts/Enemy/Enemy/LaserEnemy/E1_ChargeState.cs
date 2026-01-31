using UnityEngine;

public class E1_ChargeState : EnemyChargeState
{
    private E1_Base enemy;
    public E1_ChargeState(EnemyTemplete templete, StateMachine stateMachine, string animBoolName, EnemyData enemyData, E1_Base enemy) : base(templete, stateMachine, animBoolName, enemyData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(!isDetectingLedge || isDetectingWall)
        {
            StateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if (ChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                StateMachine.ChangeState(enemy.detectedState);
            }
        }
    }
}
