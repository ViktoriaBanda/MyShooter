using SimpleEventBus.Disposables;
using UnityEngine;

public class Waiting : MonoBehaviour, IState
{
    private static readonly int IsMove = Animator.StringToHash("isMove");

    private StateMachine _stateMachine;
    
    private GameObject _player;
    
    [SerializeField]
    private Animator _animator;

    [SerializeField] 
    private EnemyArgRegion _enemyArgRegion;

    private CompositeDisposable _subscriptions;

    private bool _isMoving = true;
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _player = GetComponent<Enemy>().Player;
    }

    public void OnEnter()
    {
        _enemyArgRegion.OnPlayerGetIntoArgRegion += _stateMachine.Enter<Moving>;
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<BulletHitEvent>(BulletHitEventHandler)
        };
        
        _animator.SetBool(IsMove, false);
        _isMoving = false;
    }

    public void OnExit()
    {
        _isMoving = true;
        _enemyArgRegion.OnPlayerGetIntoArgRegion -= _stateMachine.Enter<Moving>;
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
