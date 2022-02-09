using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] 
    private Player _player;

    [SerializeField] 
    private HealthBar _healthBar;

    [SerializeField] 
    private float _healthReductionValue = 5;
    
    private float _currentHealth;
    
    private ColorBlock _healthColor;
    
    private CompositeDisposable _subscriptions;
    
    private void Awake()
    {
        ResetHealthBar();

        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<PlayerGetDamageEvent>(PlayerGetDamageEventHandler),  
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler)
        };
    }

    private void ResetHealthBar()
    {
        _currentHealth = _player.GetMaxHealth();
        _healthBar.Initialize(_currentHealth, Color.green);
        _healthBar.gameObject.SetActive(true);
    }

    private void PlayerGetDamageEventHandler(PlayerGetDamageEvent eventData)
    {
        _currentHealth -= _healthReductionValue;
        
        if (_currentHealth <= 0)
        {
            EventStreams.Game.Publish(new PlayerDiedEvent());
            _healthBar.gameObject.SetActive(false);
            return;
        }
    
        if (_currentHealth >= 10)
        {
            _healthBar.Initialize(_currentHealth, Color.yellow);
            return;
        }
    
        _healthBar.Initialize(_currentHealth, Color.red);
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
