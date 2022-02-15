using UnityEngine;

public class FreeWalk : MonoBehaviour,IState
{
    [SerializeField] 
    private AgrRegion agrRegion;
    
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
        agrRegion.OnEnemyGetIntoAgrRegion += ChangeState;
    }

    public void OnExit()
    {
        agrRegion.OnEnemyGetIntoAgrRegion -= ChangeState;
    }

    private void ChangeState(GameObject enemy)
    {
        _stateMachine.Enter<Shooting>();
    }
}
