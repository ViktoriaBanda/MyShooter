using System.Collections;
using SimpleEventBus.Disposables;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
     [SerializeField]
     private GameObject _particleZombie;
     
     [SerializeField]
     private GameObject _particleBomb;

     private ParticlesPool _zombieParticlesPool;

     private CompositeDisposable _subscriptions;
     
     private void Awake()
     {
         _zombieParticlesPool = new ParticlesPool(_particleZombie, 5);
         
         _subscriptions = new CompositeDisposable
         {
             EventStreams.Game.Subscribe<EnemyDiedEvent>(EnemyDiedEventHandler),
             EventStreams.Game.Subscribe<BombExplodeEvent>(BombExplodeEventHandler)
         };
     }

     private void EnemyDiedEventHandler(EnemyDiedEvent eventData)
     {
         StartCoroutine(PlayParticles(eventData.Enemy.transform));
     }
     
     private void BombExplodeEventHandler(BombExplodeEvent eventData)
     {
         StartCoroutine(PlayParticles(eventData.Bomb.transform));
     }

     private IEnumerator PlayParticles(Transform spawnPosition)
     {
         GameObject particleForSpawn;
         
         if (spawnPosition.gameObject.CompareTag(GlobalConstants.ENEMY_TAG))
         {
             particleForSpawn = _zombieParticlesPool.Take();
         }
         else
         {
             particleForSpawn = _particleBomb;
         }
         
         particleForSpawn.transform.position = spawnPosition.position;

         var particles = particleForSpawn.GetComponentsInChildren<ParticleSystem>();
         
         foreach (var particleSystem in particles)
         {
             particleSystem.Play();
         }

         yield return new WaitForSeconds(1);

         if (particleForSpawn != _particleBomb)
         {
             _zombieParticlesPool.Release(particleForSpawn);
         }
     }

     private void OnDestroy()
     {
         _subscriptions.Dispose();
     }
}
