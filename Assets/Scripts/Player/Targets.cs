using System.Collections.Generic;
using Enemies;
using SimpleEventBus.Disposables;
using UnityEngine;

public class Targets : MonoBehaviour
{
    public List<Zombie> Enemies => _enemies;
    
    [SerializeField] 
    private AgrRegion _agrRegion;
    
    private List<Zombie> _enemies = new();
    
    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _agrRegion.OnEnemyGetIntoAgrRegion += AddEnemy;
        _agrRegion.OnEnemyGetOutAgrRegion += RemoveEnemy;
        
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
    
    public Zombie FindNearestEnemy()
    {
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
    
    private void AddEnemy(Zombie enemy)
    {
        _enemies.Add(enemy);
    }
    
    private void RemoveEnemy(Zombie enemy)
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
