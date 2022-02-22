using SimpleEventBus.Disposables;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private StateMachine _stateMachine;
    private CompositeDisposable _subscriptions;
    
    private void Start()
    {
        _stateMachine = new StateMachine
        (
            GetComponent<FreeWalkState>(),
            GetComponent<ShootingState>(),
            GetComponent<PlayerDeathState>()
        );
        
        _stateMachine.Initialize();
        _stateMachine.Enter<FreeWalkState>();
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<PlayerDiedEvent>(PlayerDiedEventHandler),
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler)
        };
    }

    private void PlayerDiedEventHandler(PlayerDiedEvent eventData)
    {
        _stateMachine.Enter<PlayerDeathState>();
    }
    
    private void GameStartEventHandler(GameStartEvent eventData)
    {
        _stateMachine.Enter<FreeWalkState>();
    }

    private void OnDestroy()
    {
        if (_subscriptions != null)
        {
             _subscriptions.Dispose();
        }
    }
}
