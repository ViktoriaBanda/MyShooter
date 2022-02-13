using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] 
    private CharacteristicManager _characteristicManager;
    
    [SerializeField] 
    private HealthBar _healthBar;

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

    private void Update()
    {
        _currentHealth = _characteristicManager.GetCharacteristicByName(_healthBar.Name).GetCurrentValue();
        
        switch (_currentHealth)
        {
            case <= 0:
                EventStreams.Game.Publish(new PlayerDiedEvent());
                _healthBar.gameObject.SetActive(false);
                return;
            case >= 20:
                _healthBar.Initialize(_currentHealth, Color.green);
                return;
            case >= 10:
                _healthBar.Initialize(_currentHealth, Color.yellow);
                return;
            default:
                _healthBar.Initialize(_currentHealth, Color.red);
                break;
        }
    }

    private void ResetHealthBar()
    {
        _currentHealth = _characteristicManager.GetCharacteristicByName(_healthBar.Name).GetMaxValue();
        _characteristicManager.GetCharacteristicByName(_healthBar.Name).SetValue(_currentHealth);
        _healthBar.Initialize(_currentHealth, Color.green);
        _healthBar.gameObject.SetActive(true);
    }

    private void PlayerTakesDamageEventHandler(PlayerTakesDamageEvent eventData)
    {
        _currentHealth -= _healthReductionValue;
        _characteristicManager.GetCharacteristicByName(_healthBar.Name).SetValue(_currentHealth);
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
