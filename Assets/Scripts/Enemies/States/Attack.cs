using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour, IState
{
    private static readonly int IsAttack = Animator.StringToHash("isAttack");
    
    private StateMachine _stateMachine;
    
    [SerializeField]
    private Animator _animator;
    
    private GameObject _player;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _player = GetComponent<Enemy>().Player;
    }

    public void OnEnter()
    {
        _animator.SetBool(IsAttack, true);
    }

    public void OnExit()
    {
        _animator.SetBool(IsAttack, false);
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            _stateMachine.Enter<Moving>();
        }
    }
}
