using SimpleEventBus.Disposables;
using UnityEngine;

public class FreeWalk : MonoBehaviour,IState
{
    private static readonly int IsMove = Animator.StringToHash("isMove");
    
    [SerializeField]
    private Animator _animator;
    
    [SerializeField] 
    private float _speed;
    
    [SerializeField]
    private float _agrRegion = 10;

    [SerializeField] 
    private PlayerArgRegion _playerArgRegion;
    
    
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
            EventStreams.Game.Subscribe<PlayerDiedEvent>(PlayerDiedEventHandler)
        };
        _playerArgRegion.OnEnemyGetIntoArgRegion += _stateMachine.Enter<Shooting>;
    }

    public void OnExit()
    {
        _subscriptions.Dispose();
        _playerArgRegion.OnEnemyGetIntoArgRegion -= _stateMachine.Enter<Shooting>;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -1, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 1, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            _animator.SetBool(IsMove, true);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            return;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _animator.SetBool(IsMove, true);
            transform.Translate(Vector3.back * Time.deltaTime * _speed);
            return;
        }
        
        _animator.SetBool(IsMove, false);
    }
    
    private void PlayerDiedEventHandler(PlayerDiedEvent obj)
    {
        _stateMachine.Enter<PlayerDeath>();
    }
}
