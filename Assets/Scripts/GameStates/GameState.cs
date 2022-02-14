using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour, IState
{
    private StateMachine _stateMachine;

    private CompositeDisposable _subscriptions;
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        SceneManager.LoadScene(GlobalConstants.GAME_SCENE);

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
    
    public void UpdateState()
    {
        
    }

}
