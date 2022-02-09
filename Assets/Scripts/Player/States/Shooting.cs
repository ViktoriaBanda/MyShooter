using System.Collections.Generic;
using System.Linq;
using SimpleEventBus.Disposables;
using UnityEngine;

public class Shooting : MonoBehaviour,IState
{
    private static readonly int IsShoot = Animator.StringToHash("isShoot");
    
    [SerializeField]
    private Animator _animator;
    
    [SerializeField] 
    private PlayerArgRegion _playerArgRegion;

    private bool _isShoot;
    
    [SerializeField]
    private float _reloadTimer = 0.2f;
    private float _currentTimer;

    private List<GameObject> _enemies;
    
    private StateMachine _stateMachine;

    private CompositeDisposable _subscriptions;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _currentTimer = 0;
        _enemies = new List<GameObject>();
        _isShoot = true;
        _playerArgRegion.OnEnemyInArgRegion += AddEnemy;

        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<EnemyDiedEvent>(EnemyDiedEventHandler),
            EventStreams.Game.Subscribe<PlayerDiedEvent>(PlayerDiedEventHandler)
        };
    }

    public void OnExit()
    {
        _isShoot = false;
        _playerArgRegion.OnEnemyInArgRegion -= AddEnemy;
        _subscriptions.Dispose();
    }

    private void Update()
    {
        if (!_isShoot)
        {
            _animator.SetBool(IsShoot, false);
            return;
        }
        
        if (_enemies.Count == 0)
        {
            _stateMachine.Enter<FreeWalk>();
        }
        
        if (_currentTimer > 0)
        {
            _animator.SetBool(IsShoot, false);
            _currentTimer -= Time.deltaTime;
            return;
        }

        if (_enemies.Count > 0)
        {
            _animator.SetBool(IsShoot, true);
            
            EventStreams.Game.Publish(new PlayerShootingEvent(_enemies[0]));
            
            _currentTimer = _reloadTimer;
        }
    }

    private void AddEnemy(GameObject enemy)
    {
        if (_enemies.Any(currentEnemy => currentEnemy == enemy))
        {
            return;
        }
        
        _enemies.Add(enemy);
    }

    private void EnemyDiedEventHandler(EnemyDiedEvent eventData)
    {
        _enemies.RemoveAt(0);
    }
    
    private void PlayerDiedEventHandler(PlayerDiedEvent obj)
    {
        _stateMachine.Enter<PlayerDeath>();
    }
}
