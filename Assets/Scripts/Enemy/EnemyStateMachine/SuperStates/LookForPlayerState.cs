using UnityEngine;

public class LookForPlayerState : State
{
    protected bool Immediateturn;
    protected bool isPlayerInMinAgroRange;
    protected bool isturndone;
    protected bool isturntimedone;

    protected float lastturntime;
    protected int turncount;
    public LookForPlayerState(EnemyTemplete templete, StateMachine stateMachine, string animBoolName, EnemyData enemyData) : base(templete, stateMachine, animBoolName, enemyData)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Immediateturn)
        {
            Templete.Flip();
            lastturntime = Time.time;
            turncount++;
            Immediateturn = false;
        }
        else if (Time.time >= lastturntime + enemyData.timeBetweenTurns && !isturndone)
        {
            Templete.Flip();
            lastturntime = Time.time;
            turncount++;
        }

        if (turncount >= enemyData.EnemyTurns)
        {
            isturndone = true;
        }

        if(Time.time >= lastturntime + enemyData.timeBetweenTurns && isturndone)
        {
            isturntimedone = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = Templete.CheckPlayerInMinAgroRange();
    }

    public void SetImmediateTurn(bool turn)
    {
        Immediateturn = turn;
    }
}
