using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public State InitialState;

    protected State CurrentState;

    private void Start()
    {
        CurrentState = InitialState;
    }

    // WARNING: CALL 
    public void StateUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateUpdate();
        }
    }

    public void ChangeState(State newState)
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateExit();
        }
        CurrentState = newState;
        CurrentState.OnStateEnter(this);
    }
}
