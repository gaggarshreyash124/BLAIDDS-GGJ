using UnityEngine;

public class E1_LookForPlayerState : LookForPlayerState
{
    private E1_Base enemy;
    public E1_LookForPlayerState(EnemyTemplete templete, StateMachine stateMachine, string animBoolName, EnemyData enemyData, E1_Base enemy) : base(templete, stateMachine, animBoolName, enemyData)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        turncount = 0;
        isturndone = false;
        isturntimedone = false;
        Immediateturn = true;
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
        else if (isturntimedone)
        {
            StateMachine.ChangeState(enemy.moveState);
        }
    }
}
