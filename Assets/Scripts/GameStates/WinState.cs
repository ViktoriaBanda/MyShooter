using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour, IState
{
    private StateMachine _stateMachine;
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        Debug.Log("WinState");
    }

    public void OnExit()
    {
        
    }
}
