using SimpleEventBus.Disposables;
using UnityEngine;

public class Attack : MonoBehaviour, IState
{
    private static readonly int IsAttack = Animator.StringToHash("isAttack");
    
    private StateMachine _stateMachine;
    
    [SerializeField]
    private Animator _animator;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _animator.SetBool(IsAttack, true);
    }

    public void OnExit()
    {
        _animator.SetBool(IsAttack, false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            EventStreams.Game.Publish(new PlayerTakesDamageEvent());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            _stateMachine.Enter<Moving>();
        }
    }
}
