using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private StateMachine _stateMachine;
    
    [SerializeField]
    private PlayerSettingsProvider _playerSettingsProvider;
    
    private CompositeDisposable _subscriptions;

    private Button _backButton;
    
    private void Awake()
    {
        //Инициализация ScriptableObject
        _playerSettingsProvider.Initialize();
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<GameSceneLoadedEvent>(GameSceneLoadedEventHandler)
        };

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
    
    private void GameSceneLoadedEventHandler(GameSceneLoadedEvent eventData)
    {
        FindBackButton();
    }

    private void FindBackButton()
    {
        _backButton = FindObjectOfType<BackButton>().GetComponent<UnityEngine.UI.Button>();
        _backButton.onClick.AddListener(EnterLobbyState);
    }

    private void EnterLobbyState()
    {
        _stateMachine.Enter<LobbyState>();
        _backButton.onClick.RemoveListener(_stateMachine.Enter<LobbyState>);
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
