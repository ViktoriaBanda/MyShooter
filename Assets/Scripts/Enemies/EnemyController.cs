using BehaviorDesigner.Runtime;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Player => _player;

    
    private GameObject _player;

    private Behavior _behaviorTree;

    private void Awake()
    {
        _behaviorTree = gameObject.GetComponent<BehaviorTree>();
    }

    public void Initialize(GameObject player)
    {
        _player = player;
        _behaviorTree.FindTask<Seek>().Target = _player.GetComponent<Player>();
    }
}
