using SimpleEventBus.Disposables;
using UnityEngine;

public class EnemyController : MonoBehaviour
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
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler),
            EventStreams.Game.Subscribe<EnemyTakesDamageEvent>(EnemyTakesDamageEventHandler)
        };
    }

    public void Initialize(GameObject player)
    {
        _player = player;
    }
    
    private void GameStartEventHandler(GameStartEvent eventData)
    {
        _stateMachine.Enter<Waiting>();
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
    
    private void EnemyTakesDamageEventHandler(EnemyTakesDamageEvent eventData)
    {
        if (eventData.Enemy == gameObject)
        {
            _stateMachine.Enter<Death>();
        }
    }
}
