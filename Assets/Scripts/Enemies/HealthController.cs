using SimpleEventBus.Disposables;
using UnityEngine;

namespace Enemies
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] 
        private Zombie _zombie;

        [SerializeField]
        private float _maxHealth = 10f;

        private float _currentHealth;

        private CompositeDisposable _subscriptions;
        
        private void Start()
        {
            ResetHealth();
            _zombie.OnTakeDamage += DecreaseHealth;
            
            _subscriptions = new CompositeDisposable
            {
                EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler)
            };
        }

        private void DecreaseHealth(float damageValue)
        {
            _currentHealth -= damageValue;

            if (_currentHealth <= 0)
            {
                EventStreams.Game.Publish(new EnemyDiedEvent(_zombie));
            }
        }
    
        private void GameStartEventHandler(GameStartEvent eventData)
        {
            ResetHealth();
        }
    
        private void ResetHealth()
        {
            _currentHealth = _maxHealth;
        }
    
        private void OnDestroy()
        {
            _subscriptions.Dispose();
            _zombie.OnTakeDamage -= DecreaseHealth;
        }
    }
}