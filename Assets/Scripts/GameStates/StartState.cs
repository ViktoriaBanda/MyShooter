using UnityEngine;
using UnityEngine.UI;

public class StartState : MonoBehaviour, IState
{
    [SerializeField] 
    private Button _startButton;
    
    private StateMachine _stateMachine;
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _startButton.onClick.AddListener(_stateMachine.Enter<GameState>);
    }

    public void OnExit()
    {
        _startButton.onClick.RemoveListener(_stateMachine.Enter<GameState>);
    }
    
    public void UpdateState()
    {
        
    }
}
