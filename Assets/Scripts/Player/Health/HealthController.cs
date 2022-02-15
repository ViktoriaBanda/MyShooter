using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] 
    private CharacteristicManager _characteristicManager;

    [SerializeField] 
    private float _healthReductionValue = 5;
    
    private float _currentHealth;

    private ColorBlock _healthColor;
    
    private CompositeDisposable _subscriptions;
    
    private void Start()
    {
        ResetHealthBar();

        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<PlayerTakesDamageEvent>(PlayerTakesDamageEventHandler),  
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler)
        };
    }

    private void ResetHealthBar()
    {
        _currentHealth = _characteristicManager.GetCharacteristicByType(CharacteristicType.Health).GetMaxValue();
        _characteristicManager.GetCharacteristicByType(CharacteristicType.Health).SetValue(_currentHealth);
    }

    private void PlayerTakesDamageEventHandler(PlayerTakesDamageEvent eventData)
    {
        _currentHealth = _characteristicManager.GetCharacteristicByType(CharacteristicType.Health).GetCurrentValue();
        _currentHealth -= _healthReductionValue;
        _characteristicManager.GetCharacteristicByType(CharacteristicType.Health).SetValue(_currentHealth);

        if (_currentHealth <= 0)
        {
            EventStreams.Game.Publish(new PlayerDiedEvent());
        }
    }
    
    private void GameStartEventHandler(GameStartEvent eventData)
    {
        ResetHealthBar();
    }
    
    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
