using UnityEngine;

public class WaitingState : MonoBehaviour, IState
{
    private static readonly int IsMove = Animator.StringToHash("isMove");

    private StateMachine _stateMachine;
    
    [SerializeField]
    private Animator _animator;

    [SerializeField] 
    private EnemyAgrRegion _enemyAgrRegion;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _enemyAgrRegion.OnPlayerGetIntoAgrRegion += _stateMachine.Enter<MovingState>;
        
        _animator.SetBool(IsMove, false);
    }

    public void OnExit()
    {
        _enemyAgrRegion.OnPlayerGetIntoAgrRegion -= _stateMachine.Enter<MovingState>;
    }
}
