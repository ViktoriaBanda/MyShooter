using UnityEngine;

public class FreeWalk : MonoBehaviour,IState
{
    [SerializeField] 
    private PlayerAgrRegion playerAgrRegion;
    
    [SerializeField] 
    private MovementBehaviour _movementBehaviour;

    private StateMachine _stateMachine;
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _movementBehaviour.StartMove();
        playerAgrRegion.OnEnemyGetIntoAgrRegion += ChangeState;
    }

    public void OnExit()
    {
        playerAgrRegion.OnEnemyGetIntoAgrRegion -= ChangeState;
    }

    private void ChangeState(GameObject enemy)
    {
        _stateMachine.Enter<Shooting>();
    }
}
