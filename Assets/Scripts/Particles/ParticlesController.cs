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
         PlayParticles(_particleZombie, eventData.Enemy.transform);
     }
     
     private void BombExplodeEventHandler(BombExplodeEvent eventData)
     {
         PlayParticles(_particleBomb, eventData.Bomb.transform);
     }

     private void PlayParticles(GameObject particle, Transform spawnPosition)
     {
         particle.transform.position = spawnPosition.position;

         var particles = particle.GetComponentsInChildren<ParticleSystem>();

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
