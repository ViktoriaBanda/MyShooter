using UnityEngine;

public class Shooting : MonoBehaviour, IState
{
    private static readonly int IsShoot = Animator.StringToHash("isShoot");

    [SerializeField]
    private Animator _animator;

    [SerializeField] 
    private ShootingBehaviour _shootingBehaviour;

    private StateMachine _stateMachine;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _shootingBehaviour.StartAutoShoot();
        _shootingBehaviour.AllEnemiesHaveKilledEvent += ChangeState;
        _animator.SetBool(IsShoot, true);
    }

    public void OnExit()
    {
        _shootingBehaviour.StopAutoShoot();
        _shootingBehaviour.AllEnemiesHaveKilledEvent -= ChangeState;
        _animator.SetBool(IsShoot, false);
    }

    private void ChangeState()
    {
        _stateMachine.Enter<FreeWalk>();
    }
}
