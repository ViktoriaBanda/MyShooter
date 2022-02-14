using UnityEngine;

public class Shooting : MonoBehaviour, IState
{
    private static readonly int IsShoot = Animator.StringToHash("isShoot");

    [SerializeField]
    private Animator _animator;

    //[SerializeField] 
    //private ShootingTargets _shootingTargets;
    
    //private bool _isShoot;

    [SerializeField] 
    private ShootingBehaviour _shootingBehaviour;

    private StateMachine _stateMachine;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        //_isShoot = true;
        _shootingBehaviour.StartAutoShoot();
        _shootingBehaviour.AllEnemiesHaveKilledEvent += ChangeState;
        _animator.SetBool(IsShoot, true);
    }

    public void OnExit()
    {
        //_isShoot = false;
        _shootingBehaviour.StopAutoShoot();
        _shootingBehaviour.AllEnemiesHaveKilledEvent -= ChangeState;
        _animator.SetBool(IsShoot, false);
    }

    private void ChangeState()
    {
        _stateMachine.Enter<FreeWalk>();
    }

    private void Update()
    {
        //if (!_isShoot)
        //{
        //    _animator.SetBool(IsShoot, false);
        //    return;
        //}
        //
        //if (_shootingTargets.GetEnemiesNumber() <= 0)
        //{
        //    _stateMachine.Enter<FreeWalk>();
        //    return;
        //}
//
        //_animator.SetBool(IsShoot, true);
//
        //var nearestEnemy = _shootingTargets.FindNearestEnemy();
        //transform.LookAt(nearestEnemy.transform);
        //
        //EventStreams.Game.Publish(new PlayerShootingEvent(nearestEnemy));
    }
}
