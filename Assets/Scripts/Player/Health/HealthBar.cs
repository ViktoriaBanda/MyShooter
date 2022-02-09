using System;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image _health;

    [SerializeField] 
    private float _playerHealth = 20;

    private float _currentHealth;

    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _currentHealth = _playerHealth;
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<PlayerGetDamageEvent>(PlayerGetDamageEventHandler),  
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler)
        };
        _health.color = Color.green;
    }

    private void GameStartEventHandler(GameStartEvent eventData)
    {
        _currentHealth = _playerHealth;
        _health.color = Color.green;
    }

    private void PlayerGetDamageEventHandler(PlayerGetDamageEvent eventData)
    {
        _currentHealth -= 5;
       
        if (_currentHealth <= 0)
        {
            EventStreams.Game.Publish(new PlayerDiedEvent());
        }

        if (_currentHealth >= 10)
        {
            _health.color = Color.yellow;
            return;
        }
        
        _health.color = Color.red;
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
