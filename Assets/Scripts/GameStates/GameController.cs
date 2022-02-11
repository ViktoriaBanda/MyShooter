using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private StateMachine _stateMachine;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        _stateMachine = new StateMachine
        (
            GetComponent<StartState>(),
            GetComponent<GameState>(),
            GetComponent<GameOverState>(),
            GetComponent<WinState>()
        );

        _stateMachine.Initialize();
        _stateMachine.Enter<StartState>();
    }
}
