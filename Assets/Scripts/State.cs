using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour 
{
    [HideInInspector] public StateController StateController;
    public void OnStateEnter(StateController stateController)
    {
        // Code placed here will always run
        StateController = stateController;
        OnEnter();
    }
    protected virtual void OnEnter()
    {
        // Code placed here can be overridden
    }

    public void OnStateUpdate()
    {
        // Code placed here will always run
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        // Code placed here can be overridden
    }
    public void OnStateHurt()
    {
        // Code placed here will always run
        OnHurt();
    }
    protected virtual void OnHurt()
    {
        // Code placed here can be overridden
    }
    public void OnStateExit()
    {
        // Code placed here will always run
        OnExit();
    }
    protected virtual void OnExit()
    {
        // Code placed here can be overridden
    }
}
