using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image _health;

    [SerializeField] 
    private float _playerHealth = 20;

    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<PlayerGetDamageEvent>(PlayerGetDamageEventHandler),  
        };
        _health.color = Color.green;
    }

    private void PlayerGetDamageEventHandler(PlayerGetDamageEvent eventData)
    {
        _playerHealth -= 5;

        if (_playerHealth <= 0)
        {
            EventStreams.Game.Publish(new PlayerDiedEvent());
        }

        if (_playerHealth >= 10)
        {
            _health.color = Color.yellow;
            return;
        }
        
        _health.color = Color.red;
    }
}
