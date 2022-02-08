using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Player => _player;

    private StateMachine _stateMachine;
    private GameObject _player;

    private void Start()
    {
        _stateMachine = new StateMachine
        (
            GetComponent<Waiting>(),
            GetComponent<Moving>(),
            GetComponent<Attack>(),
            GetComponent<Death>()
        );

        _stateMachine.Initialize();
        _stateMachine.Enter<Waiting>();
    }

    public void Initialize(GameObject player)
    {
        _player = player;
    }
}
