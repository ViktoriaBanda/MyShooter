using System.Collections;
using System.Collections.Generic;
using SimpleEventBus.Disposables;
using UnityEngine;

public class WeaponParticlesController : MonoBehaviour
{
    [SerializeField]
    private GameObject _particle;
    
    private CompositeDisposable _subscriptions;
    
    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
           EventStreams.Game.Subscribe<PlayerShootingEvent>(PlayerShootingEventHandler)
        };
    }
    
    private void PlayerShootingEventHandler(PlayerShootingEvent eventData)
    {
        _particle.transform.position = transform.position;
         
        var particles = _particle.GetComponent<ParticleSystem>();
        particles.Play();
    }
    
    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
