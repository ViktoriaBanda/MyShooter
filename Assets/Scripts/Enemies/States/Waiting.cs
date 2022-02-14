using SimpleEventBus.Disposables;
using UnityEngine;

public class Waiting : MonoBehaviour, IState
{
    private static readonly int IsMove = Animator.StringToHash("isMove");

    private StateMachine _stateMachine;
    
    [SerializeField]
    private Animator _animator;

    [SerializeField] 
    private EnemyAgrRegion _enemyAgrRegion;

    private CompositeDisposable _subscriptions;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _enemyAgrRegion.OnPlayerGetIntoAgrRegion += _stateMachine.Enter<Moving>;
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<EnemyTakesDamageEvent>(EnemyTakesDamageEventHandler)
        };
        
        _animator.SetBool(IsMove, false);
    }

    public void OnExit()
    {
        _enemyAgrRegion.OnPlayerGetIntoAgrRegion -= _stateMachine.Enter<Moving>;
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
