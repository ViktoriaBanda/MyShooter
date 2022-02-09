using System;
using SimpleEventBus.Disposables;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Player => _player;

    private StateMachine _stateMachine;
    private GameObject _player;

    private CompositeDisposable _subscriptions;
    
    private void Start()
    {
        _stateMachine = new StateMachine
        (
            GetComponent<Waiting>(),
            GetComponent<Moving>(),
            GetComponent<Attack>(),
            GetComponent<Death>()
        );

        _stateMachine.Initialize();
        _stateMachine.Enter<Waiting>();
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler)
        };
    }

    public void Initialize(GameObject player)
    {
        _player = player;
    }
    
    private void GameStartEventHandler(GameStartEvent enentData)
    {
        _stateMachine.Enter<Waiting>();
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
