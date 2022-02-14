using System;
using UnityEngine;

public class Death : MonoBehaviour, IState
{
    private StateMachine _stateMachine;
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
       EventStreams.Game.Publish(new EnemyDiedEvent(gameObject));
       gameObject.SetActive(false);
    }

    public void OnExit()
    {
        
    }

    public void UpdateState()
    {
        
    }
}
