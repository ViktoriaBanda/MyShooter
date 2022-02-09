using SimpleEventBus.Disposables;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private Transform _playerSpawnPosition;
    
    private StateMachine _stateMachine;
    private CompositeDisposable _subscriptions;
    
    private void Start()
    {
        _stateMachine = new StateMachine
        (
            GetComponent<FreeWalk>(),
            GetComponent<Shooting>(),
            GetComponent<PlayerDeath>()
        );
        
        _stateMachine.Initialize();
        _stateMachine.Enter<FreeWalk>();
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler)
        };
    }
    
    private void GameStartEventHandler(GameStartEvent obj)
    {
        gameObject.transform.position = _playerSpawnPosition.position;
        gameObject.SetActive(true);
        _stateMachine.Enter<FreeWalk>();
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
