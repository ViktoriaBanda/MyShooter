using System.Collections.Generic;
using SimpleEventBus.Disposables;
using UnityEngine;

public class BombTargets : MonoBehaviour
{
    public List<GameObject> Enemies => _enemies;
    
    [SerializeField] 
    private BombAgrRegion _bombAgrRegion;
    
    private List<GameObject> _enemies = new();
    
    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _bombAgrRegion.OnEnemyGetIntoAgrRegion += AddEnemy;
        _bombAgrRegion.OnEnemyGetOutAgrRegion += RemoveEnemy;
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler)
        };
    }
    
    private void AddEnemy(GameObject enemy)
    {
        _enemies.Add(enemy);
    }
    
    private void RemoveEnemy(GameObject enemy)
    {
        _enemies.Remove(enemy);
    }
    
    private void GameStartEventHandler(GameStartEvent eventData)
    {
        //мусор
        //_enemies.Clear();
        _enemies = new List<GameObject>();
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
        _bombAgrRegion.OnEnemyGetIntoAgrRegion -= AddEnemy;
        _bombAgrRegion.OnEnemyGetOutAgrRegion -= RemoveEnemy;
    }
}
