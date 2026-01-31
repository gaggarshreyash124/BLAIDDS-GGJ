using UnityEngine;

public class EnemyIdleState : State
{
    
    protected bool flipafteridle;

    protected bool isidleover;
    protected float IdleTime;
    protected bool isPlayerInMinAgroRange;

    public EnemyIdleState(EnemyTemplete Templete, StateMachine stateMachine, string animboolname, EnemyData enemyData) : base(Templete, stateMachine, animboolname, enemyData)
    {

    }
    override public void Enter()
    {
        base.Enter();
        Templete.SetVelocity(0);
        isidleover = false;
        SetRandomIdleTime();
        
    }
    override public void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= StartTime + IdleTime)
        {
            isidleover = true;
        }
    }
    override public void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }
    override public void Exit()
    {
        base.Exit();
        if (flipafteridle)
        {
            Templete.Flip();
        }
    }

    override public void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = Templete.CheckPlayerInMinAgroRange();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipafteridle = flip;
    }

    void SetRandomIdleTime()
    {
        IdleTime = Random.Range(enemyData.minIdleTime, enemyData.maxIdleTime);
    }
}
