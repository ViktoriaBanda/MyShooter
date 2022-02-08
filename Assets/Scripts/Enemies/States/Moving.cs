using UnityEngine;
using UnityEngine.AI;

public class Moving : MonoBehaviour, IState
{
    private static readonly int IsMove = Animator.StringToHash("isMove");
    
    private StateMachine _stateMachine;
    
    [SerializeField]
    private Animator _animator;
    
    [SerializeField]
    private NavMeshAgent _navMeshAgent;

    [SerializeField] 
    private Waiting _waitingState;
    
    private GameObject _player;

    private float _argRegion;

    private bool _isMoving;
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _player = GetComponent<Enemy>().Player;
        _argRegion = _waitingState.ArgRegion;
    }

    public void OnEnter()
    {
        _animator.SetBool(IsMove, true);
        _isMoving = true;
    }

    public void OnExit()
    {
        _isMoving = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            EventStreams.Game.Publish(new EnemyCollisionEvent(collision.gameObject));
            _stateMachine.Enter<Attack>();
        }
    }

    private void Update()
    {
        if (!_isMoving)
        {
            return;
        }
        _navMeshAgent.destination = _player.transform.position;
        
        if (Vector3.Distance(transform.position, _player.transform.position) > _argRegion)
        {
            _stateMachine.Enter<Waiting>();
        }
    }
}
