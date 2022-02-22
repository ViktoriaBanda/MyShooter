using BehaviorDesigner.Runtime;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Player => _player;

    private StateMachine _stateMachine;
    private GameObject _player;

    private Behavior _behaviorTree;

    //private CompositeDisposable _subscriptions;
    
    private void Awake()
    {
        _behaviorTree = gameObject.GetComponent<BehaviorTree>();
        
        _stateMachine = new StateMachine
        (
            //GetComponent<WaitingState>(),
            //GetComponent<MovingState>(),
            //GetComponent<AttackState>(),
            //GetComponent<DeathState>()
        );

        _stateMachine.Initialize();
        //_stateMachine.Enter<WaitingState>();
        
        //_subscriptions = new CompositeDisposable
        //{
        //    EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler),
        //    EventStreams.Game.Subscribe<EnemyDiedEvent>(EnemyDiedEventHandler)
        //};
    }

    public void Initialize(GameObject player)
    {
        _player = player;
        _behaviorTree.FindTask<Move>().Target = _player.GetComponent<Player>();
    }
    
    private void GameStartEventHandler(GameStartEvent eventData)
    {
        _stateMachine.Enter<WaitingState>();
    }

    private void OnDestroy()
    {
        //_subscriptions.Dispose();
    }
    
    private void EnemyDiedEventHandler(EnemyDiedEvent eventData)
    {
        if (eventData.Enemy.gameObject == gameObject)
        {
            _stateMachine.Enter<DeathState>();
        }
    }
}
