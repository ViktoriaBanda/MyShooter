using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private StateMachine _stateMachine;
    
    private void Start()
    {
        _stateMachine = new StateMachine
        (
            GetComponent<FreeWalk>(),
            GetComponent<Shooting>(),
            GetComponent<PlayerDeath>()
        );
        
        _stateMachine.Initialize();
        _stateMachine.Enter<FreeWalk>();
    }
}
