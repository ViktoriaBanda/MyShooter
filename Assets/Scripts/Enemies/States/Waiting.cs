using UnityEngine;

public class Waiting : MonoBehaviour, IState
{
    private static readonly int IsMove = Animator.StringToHash("isMove");

    public float ArgRegion => _agrRegion;

    private StateMachine _stateMachine;
    
    private GameObject _player;
    
    [SerializeField]
    private Animator _animator;
    
    [SerializeField]
    private float _agrRegion = 15;

    private bool _isMoving = true;
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _player = GetComponent<Enemy>().Player;
    }

    public void OnEnter()
    {
        _animator.SetBool(IsMove, false);
        _isMoving = false;
    }

    public void OnExit()
    {
        _isMoving = true;
    }

    private void Update()
    {
        if (_isMoving)
        {
            return;
        }
        
        if (Vector3.Distance(transform.position, _player.transform.position) <= _agrRegion)
        {
            _stateMachine.Enter<Moving>();
        }
    }
}
