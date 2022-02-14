using System.Collections.Generic;
using SimpleEventBus.Disposables;
using UnityEngine;

public class ShootingTargets : MonoBehaviour
{
    [SerializeField] 
    private PlayerAgrRegion _playerAgrRegion;
    
    private List<GameObject> _enemies = new();
    
    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _playerAgrRegion.OnEnemyGetIntoAgrRegion += AddEnemy;
        _playerAgrRegion.OnEnemyGetOutAgrRegion += RemoveEnemy;
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<EnemyDiedEvent>(EnemyDiedEventHandler),
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler)
        };
    }

    public int GetEnemiesNumber()
    {
        return _enemies.Count;
    }
    
    public GameObject FindNearestEnemy()
    {
        Debug.Log(_enemies.Count);
        var nearestEnemy = _enemies[0];
        var minDistance = Vector3.Distance(transform.position, nearestEnemy.transform.position);
        
        foreach (var enemy in _enemies)
        {
            var distanceToCurrentEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToCurrentEnemy < minDistance)
            {
                nearestEnemy = enemy;
                minDistance = distanceToCurrentEnemy;
            }
        }

        return nearestEnemy;
    }
    
    private void AddEnemy(GameObject enemy)
    {
        _enemies.Add(enemy);
    }
    
    private void RemoveEnemy(GameObject enemy)
    {
        _enemies.Remove(enemy);
    }
    
    private void EnemyDiedEventHandler(EnemyDiedEvent eventData)
    {
        _enemies.Remove(eventData.Enemy);
    }
    
    private void GameStartEventHandler(GameStartEvent eventData)
    {
        _enemies.Clear();
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
