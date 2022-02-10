using SimpleEventBus.Disposables;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
     [SerializeField]
     private GameObject _particle;
     
     private CompositeDisposable _subscriptions;
     
     private void Awake()
     {
         _subscriptions = new CompositeDisposable
         {
             EventStreams.Game.Subscribe<EnemyDiedEvent>(EnemyDiedEventHandler),
         };
     }

     private void EnemyDiedEventHandler(EnemyDiedEvent eventData)
     {
         _particle.transform.position = eventData.Enemy.transform.position;
         
         var particles = _particle.GetComponentsInChildren<ParticleSystem>();
         
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
