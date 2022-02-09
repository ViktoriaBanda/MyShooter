using SimpleEventBus.Disposables;
using UnityEngine;

public class Death : MonoBehaviour, IState
{
    private StateMachine _stateMachine;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        gameObject.SetActive(false);
        EventStreams.Game.Publish(new EnemyDiedEvent(gameObject));
    }

    public void OnExit()
    {
        
    }
}
