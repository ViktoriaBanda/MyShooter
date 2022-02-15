using System;
using SimpleEventBus.Disposables;
using UnityEngine;

namespace Enemies
{
    public class Zombie : MonoBehaviour
    {
        public event Action<float> OnTakeDamage;
        
        [SerializeField] 
        private CharacteristicManager _characteristicManager;
    
        [SerializeField]
        private Characteristic[] _characteristics;
        
        private CompositeDisposable _subscriptions;

        private void Awake()
        {
            _characteristicManager.Initialize(_characteristics);
            _subscriptions = new CompositeDisposable
            {
                EventStreams.Game.Subscribe<EnemyTakesDamageEvent>(EnemyTakesDamageEventHandler)
            };
        }

        private void EnemyTakesDamageEventHandler(EnemyTakesDamageEvent eventData)
        {
            if (eventData.Enemy == gameObject)
            {
                OnTakeDamage?.Invoke(eventData.DamageValue);
            }
        }

        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }
    }
    
}
