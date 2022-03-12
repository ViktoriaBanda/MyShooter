using SimpleEventBus.Events;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using CompositeDisposable = SimpleEventBus.Disposables.CompositeDisposable;

public class HealthController : MonoBehaviour
{
    public BehaviorSubject<float> CurrentHealth;
    public float MaxHealth { get; private set; }

    [SerializeField] 
    private CharacteristicManager _characteristicManager;

    private ColorBlock _healthColor;
    
    private CompositeDisposable _subscriptions;
    
    private void Start()
    {
        MaxHealth = _characteristicManager.GetCharacteristicByType(CharacteristicType.Health).GetMaxValue();
        
        CurrentHealth = new BehaviorSubject<float>(MaxHealth);
        
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
        CurrentHealth.OnNext( CurrentHealth.Value - eventData.DamageValue);
        
        currentHealth.SetValue(CurrentHealth.Value);
        
        if (CurrentHealth.Value <= 0)
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
        CurrentHealth.OnNext(MaxHealth);
        var currentHealth = _characteristicManager.GetCharacteristicByType(CharacteristicType.Health);
        currentHealth.SetValue(CurrentHealth.Value);
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}