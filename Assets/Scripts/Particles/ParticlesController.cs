using SimpleEventBus.Disposables;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
     [SerializeField]
     private GameObject _particleZombie;
     
     [SerializeField]
     private GameObject _particleBomb;
     
     private CompositeDisposable _subscriptions;
     
     private void Awake()
     {
         _subscriptions = new CompositeDisposable
         {
             EventStreams.Game.Subscribe<EnemyDiedEvent>(EnemyDiedEventHandler),
             EventStreams.Game.Subscribe<BombExplodeEvent>(BombExplodeEventHandler)
         };
     }

     private void EnemyDiedEventHandler(EnemyDiedEvent eventData)
     {
         _particleZombie.transform.position = eventData.Enemy.transform.position;
         
         var particles = _particleZombie.GetComponentsInChildren<ParticleSystem>();
         
         foreach (var particleSystem in particles)
         {
             particleSystem.Play();
         }
     }
     
     private void BombExplodeEventHandler(BombExplodeEvent eventData)
     {
        _particleBomb.transform.position = eventData.Bomb.transform.position;
         var particles = _particleBomb.GetComponentsInChildren<ParticleSystem>();
         
         foreach (var particleSystem in particles)
         {
             particleSystem.Play();
         }
     }
     
     private void OnDestroy()
     {
         _subscriptions.Dispose();
     }
}
