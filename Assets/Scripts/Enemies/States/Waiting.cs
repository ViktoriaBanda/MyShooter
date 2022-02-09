using SimpleEventBus.Disposables;
using UnityEngine;

public class Waiting : MonoBehaviour, IState
{
    private static readonly int IsMove = Animator.StringToHash("isMove");

    private StateMachine _stateMachine;
    
    [SerializeField]
    private Animator _animator;

    [SerializeField] 
    private EnemyAgrRegion enemyAgrRegion;

    private CompositeDisposable _subscriptions;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        enemyAgrRegion.OnPlayerGetIntoArgRegion += _stateMachine.Enter<Moving>;
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<BulletHitEvent>(BulletHitEventHandler)
        };
        
        _animator.SetBool(IsMove, false);
    }

    public void OnExit()
    {
        enemyAgrRegion.OnPlayerGetIntoArgRegion -= _stateMachine.Enter<Moving>;
        _subscriptions.Dispose();
    }

    private void BulletHitEventHandler(BulletHitEvent eventData)
    {
        if (eventData.HittedObject == gameObject)
        {
            _stateMachine.Enter<Death>();
        }
    }
}
