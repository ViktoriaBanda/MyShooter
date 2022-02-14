using System.Collections;
using SimpleEventBus.Disposables;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
     [SerializeField]
     private GameObject _particleZombie;
     
     [SerializeField]
     private GameObject _particleBomb;

     [SerializeField] 
     private int _poolSize = 20;
     
     private GameObjectsPool _gameObjectsPool;

     private CompositeDisposable _subscriptions;
     
     private void Awake()
     {
         _gameObjectsPool = new GameObjectsPool(_poolSize, _particleZombie, _particleBomb);
         
         _subscriptions = new CompositeDisposable
         {
             EventStreams.Game.Subscribe<EnemyDiedEvent>(EnemyDiedEventHandler),
             EventStreams.Game.Subscribe<BombExplodeEvent>(BombExplodeEventHandler)
         };
     }

     private void EnemyDiedEventHandler(EnemyDiedEvent eventData)
     {
         StartCoroutine(PlayParticles(_particleZombie, eventData.Enemy.transform));
     }
     
     private void BombExplodeEventHandler(BombExplodeEvent eventData)
     {
         StartCoroutine(PlayParticles(_particleBomb, eventData.Bomb.transform));
     }

     private IEnumerator PlayParticles(GameObject particle, Transform spawnPosition)
     {
         var particleForSpawn = _gameObjectsPool.Get(particle);
         
         particleForSpawn.transform.position = spawnPosition.position;

         var particles = particleForSpawn.GetComponentsInChildren<ParticleSystem>();
         
         foreach (var particleSystem in particles)
         {
             particleSystem.Play();
         }

         yield return new WaitForSeconds(2);

         _gameObjectsPool.Release(particle, particleForSpawn);
     }

     private void OnDestroy()
     {
         _subscriptions.Dispose();
     }
}
