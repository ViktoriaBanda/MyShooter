using SimpleEventBus.Disposables;
using UnityEngine;

public class GameState : MonoBehaviour, IState
{
    private StateMachine _stateMachine;

    private CompositeDisposable _subscriptions;
    
    public Player Player { get; set; }
    
    public Vector3 SpawnPoint { get; set; } 
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        Player.transform.position = SpawnPoint;
        Player.transform.rotation = Quaternion.Euler(0, -90, 0);
           
        Player.gameObject.SetActive(true);
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<PlayerDiedEvent>(PlayerDiedEventHandler),
            EventStreams.Game.Subscribe<LevelWinEvent>(LevelWinEventHandler),
        };
    }

    public void OnExit()
    {
        _subscriptions.Dispose();
    }
    
    private void PlayerDiedEventHandler(PlayerDiedEvent obj)
    {
        _stateMachine.Enter<GameOverState>();
    }
    
    private void LevelWinEventHandler(LevelWinEvent obj)
    {
        _stateMachine.Enter<WinState>();
    }
}
