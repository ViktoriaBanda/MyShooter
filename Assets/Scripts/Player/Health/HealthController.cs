using SimpleEventBus.Disposables;
using SimpleEventBus.Events;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] 
    private CharacteristicManager _characteristicManager;

    private float _currentHealth;
    private float _maxHealth;

    private ColorBlock _healthColor;
    
    private CompositeDisposable _subscriptions;
    
    private void Start()
    {
        _maxHealth = _characteristicManager.GetCharacteristicByType(CharacteristicType.Health).GetMaxValue();
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<PlayerTakesDamageEvent>(PlayerTakesDamageEventHandler),  
            EventStreams.Game.Subscribe<GameStartEvent>(ResetHealth),
            EventStreams.Game.Subscribe<HealthBuffAchieveEvent>(ResetHealth)
        };
    }

    private void PlayerTakesDamageEventHandler(PlayerTakesDamageEvent eventData)
    {
        var currentHealth = _characteristicManager.GetCharacteristicByType(CharacteristicType.Health);
        _currentHealth = currentHealth.GetCurrentValue();
        
        _currentHealth -= eventData.DamageValue;
        currentHealth.SetValue(_currentHealth);
        
        if (_currentHealth <= 0)
        {
            EventStreams.Game.Publish(new PlayerDiedEvent());
        }
    }

    private void ResetHealth(EventBase eventData)
    {
        ResetHealth();
    }

    private void ResetHealth()
    {
        _currentHealth = _maxHealth;
        var currentHealth = _characteristicManager.GetCharacteristicByType(CharacteristicType.Health);
        currentHealth.SetValue(_currentHealth);
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
