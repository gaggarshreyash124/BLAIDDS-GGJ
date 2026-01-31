using UnityEngine;
using UnityEngine.Rendering;

public class EnemyDetectState : State
{

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool Longrangeactiontime;
    public EnemyDetectState(EnemyTemplete templete, StateMachine stateMachine, string animBoolName, EnemyData enemyData) : base(templete, stateMachine, animBoolName, enemyData)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Longrangeactiontime = false;
        isPlayerInMaxAgroRange = Templete.CheckPlayerInMaxAgroRange();
        isPlayerInMinAgroRange = Templete.CheckPlayerInMinAgroRange();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= StartTime + enemyData.actionTime)
        {
            Longrangeactiontime = true;
        }
    }

}
