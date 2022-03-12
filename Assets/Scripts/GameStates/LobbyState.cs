using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyState : MonoBehaviour, IState
{
    private Button _startButton;
    
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
            EventStreams.Game.Subscribe<LobbySceneLoadedEvent>(LobbySceneLoadedEventHandler)    
        };

        SceneManager.LoadScene(GlobalConstants.LOBBY_SCENE);
    }

    public void OnExit()
    {
        _startButton.onClick.RemoveListener(_stateMachine.Enter<StartState>);
        _subscriptions.Dispose();
    }
    
    private void LobbySceneLoadedEventHandler(LobbySceneLoadedEvent eventData)
    {
        _startButton = FindObjectOfType<StartButton>().GetComponent<Button>();
        _startButton.onClick.AddListener(_stateMachine.Enter<StartState>);
    }
}
