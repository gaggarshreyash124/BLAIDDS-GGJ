using UnityEngine;

public class EnemyAttackState : State
{
    
    public EnemyAttackState(EnemyTemplete templete, StateMachine stateMachine, string animBoolName, EnemyData enemyData) : base(templete, stateMachine, animBoolName, enemyData)
    {
    }

}
