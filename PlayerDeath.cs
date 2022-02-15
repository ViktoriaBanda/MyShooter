using UnityEngine;

public class PlayerDeath : MonoBehaviour, IState
{
    
    [SerializeField] 
    private MovementBehaviour _movementBehaviour;
    
    private StateMachine _stateMachine;
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _movementBehaviour.StopMove();
        gameObject.SetActive(false);
    }

    public void OnExit()
    {
        
    }
}
