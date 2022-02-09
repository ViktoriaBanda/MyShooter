using System;
using SimpleEventBus.Disposables;
using TMPro;
using UnityEngine;

public class DeadEnemiesCounter : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _enemiesCounter;

    private int _enemyCounter;

    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<EnemyDiedEvent>(EnemyDiedEventHandler),
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler)
        };
    }

    private void GameStartEventHandler(GameStartEvent eventData)
    {
        _enemyCounter = 0;
        SetEnemiesCounter();
    }

    private void EnemyDiedEventHandler(EnemyDiedEvent eventData)
    {
        _enemyCounter++;
        SetEnemiesCounter();
    }
    
    private void SetEnemiesCounter()
    {
        _enemiesCounter.text = _enemyCounter.ToString();
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
