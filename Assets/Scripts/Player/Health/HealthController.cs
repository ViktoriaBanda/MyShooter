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
            EventStreams.Game.Subscribe<PlayerGetDamageEvent>(PlayerGetDamageEventHandler),  
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler)
        };
    }

    private void Update()
    {
        _currentHealth = _characteristicManager.GetCharacteristicByName(_healthBar.Name).GetCurrentValue();
        
        if (_currentHealth <= 0)
        {
            EventStreams.Game.Publish(new PlayerDiedEvent());
            _healthBar.gameObject.SetActive(false);
            return;
        }
    
        if (_currentHealth >= 20)
        {
            _healthBar.Initialize(_currentHealth, Color.green);
            return;
        }
        
        if (_currentHealth >= 10)
        {
            _healthBar.Initialize(_currentHealth, Color.yellow);
            return;
        }
    
        _healthBar.Initialize(_currentHealth, Color.red);
    }

    private void ResetHealthBar()
    {
        _currentHealth = _characteristicManager.GetCharacteristicByName(_healthBar.Name).GetMaxValue();
        _characteristicManager.GetCharacteristicByName(_healthBar.Name).SetValue(_currentHealth);
        _healthBar.Initialize(_currentHealth, Color.green);
        _healthBar.gameObject.SetActive(true);
    }

    private void PlayerGetDamageEventHandler(PlayerGetDamageEvent eventData)
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
