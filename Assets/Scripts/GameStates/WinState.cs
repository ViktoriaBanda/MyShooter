using SimpleEventBus.Disposables;
using UnityEngine;

public class WinState : MonoBehaviour, IState
{
    private StateMachine _stateMachine;
    
    private CompositeDisposable _subscriptions;
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler)
        };
    }

    public void OnExit()
    {
        _subscriptions.Dispose();
    }
    
    private void GameStartEventHandler(GameStartEvent eventData)
    {
        _stateMachine.Enter<GameState>();
    }
}
