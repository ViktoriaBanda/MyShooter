using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private StateMachine _stateMachine;
    
    [SerializeField]
    private PlayerSettingsProvider _playerSettingsProvider;
    
    private void Awake()
    {
        //Инициализация ScriptableObject
        _playerSettingsProvider.Initialize();
        
        DontDestroyOnLoad(gameObject);
        
        _stateMachine = new StateMachine
        (
            GetComponent<LobbyState>(),
            GetComponent<StartState>(),
            GetComponent<GameState>(),
            GetComponent<GameOverState>(),
            GetComponent<WinState>()
        );

        _stateMachine.Initialize();
        _stateMachine.Enter<LobbyState>();
    }
}
