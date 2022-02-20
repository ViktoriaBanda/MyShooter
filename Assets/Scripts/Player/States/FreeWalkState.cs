using Enemies;
using UnityEngine;

public class FreeWalkState : MonoBehaviour,IState
{
    [SerializeField] 
    private AgrRegion _agrRegion;
    
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
        _agrRegion.OnEnemyGetIntoAgrRegion += ChangeState;
    }

    public void OnExit()
    {
        _agrRegion.OnEnemyGetIntoAgrRegion -= ChangeState;
    }

    private void ChangeState(Zombie enemy)
    {
        _stateMachine.Enter<ShootingState>();
    }
}
