using UnityEngine;

public class State
{
    protected StateMachine StateMachine;
    protected EnemyTemplete Templete;
    protected EnemyData enemyData;
    protected float StartTime;
    protected string Animboolname;

    public State(EnemyTemplete Templete, StateMachine stateMachine, string animboolname, EnemyData enemyData)
    {
        this.Templete = Templete;
        StateMachine = stateMachine;
        Animboolname = animboolname;
        this.enemyData = enemyData;
    }
    public virtual void Enter()
    {
        StartTime = Time.time;
        Templete.anim.SetBool(Animboolname, true);

        DoChecks();
    }
    public virtual void Exit()
    {
        Templete.anim.SetBool(Animboolname, false);
    }
    public virtual void LogicUpdate()
    {
        Debug.Log("State Logic Update");
    }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {

    }

}
