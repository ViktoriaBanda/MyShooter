using SimpleEventBus.Disposables;
using UnityEngine;

public class Shooting : MonoBehaviour,IState
{
    private static readonly int IsShoot = Animator.StringToHash("isShoot");

    [SerializeField]
    private Animator _animator;

    [SerializeField] 
    private ShootingTargets _shootingTargets;
    
    private bool _isShoot;

    private StateMachine _stateMachine;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _isShoot = true;
    }

    public void OnExit()
    {
        _isShoot = false;
    }

    private void Update()
    {
        if (!_isShoot)
        {
            _animator.SetBool(IsShoot, false);
            return;
        }
        
        if (_shootingTargets.GetEnemiesNumber() <= 0)
        {
            _stateMachine.Enter<FreeWalk>();
            return;
        }

        _animator.SetBool(IsShoot, true);

        var nearestEnemy = _shootingTargets.FindNearestEnemy();
        transform.LookAt(nearestEnemy.transform);
        
        EventStreams.Game.Publish(new PlayerShootingEvent(nearestEnemy));
    }
}
