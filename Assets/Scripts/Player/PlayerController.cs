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
            GetComponent<FreeWalkState>(),
            GetComponent<ShootingState>(),
            GetComponent<PlayerDeathState>()
        );
        
        _stateMachine.Initialize();
        _stateMachine.Enter<FreeWalkState>();
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler),
            EventStreams.Game.Subscribe<PlayerDiedEvent>(PlayerDiedEventHandler)
        };
    }
    
    private void GameStartEventHandler(GameStartEvent eventData)
    {
        gameObject.transform.position = _playerSpawnPosition.position;
        gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
        
        gameObject.SetActive(true);
        _stateMachine.Enter<FreeWalkState>();
    }
    
    private void PlayerDiedEventHandler(PlayerDiedEvent obj)
    {
        _stateMachine.Enter<PlayerDeathState>();
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
