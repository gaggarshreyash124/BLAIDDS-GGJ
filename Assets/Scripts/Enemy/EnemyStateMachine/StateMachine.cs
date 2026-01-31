using UnityEngine;

public class StateMachine
{
    public State CurrentState;


    public void Initialize(State InitialState)
    {
        CurrentState = InitialState;
        CurrentState.Enter();
    }

    public void ChangeState(State NewState)
    {
        CurrentState.Exit();
        CurrentState = NewState;
        CurrentState.Enter();
    }
}
