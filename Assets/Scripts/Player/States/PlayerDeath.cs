using UnityEngine;

public class PlayerDeath : MonoBehaviour, IState
{
    private StateMachine _stateMachine;
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        gameObject.SetActive(false);
    }

    public void OnExit()
    {
        
    }
}
