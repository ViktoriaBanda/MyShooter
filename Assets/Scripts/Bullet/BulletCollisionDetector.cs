using SimpleEventBus.Disposables;
using UnityEngine;

public class BulletCollisionDetector : MonoBehaviour
{
    private CompositeDisposable _subscriptions;
    
    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<BulletHitEvent>(BulletHitEventHandler)
        };
    }

    private void BulletHitEventHandler(BulletHitEvent eventData)
    {
        if (eventData.HittedObject.CompareTag(GlobalConstants.ENEMY_TAG))
        {
            EventStreams.Game.Publish(new EnemyTakesDamageEvent(eventData.HittedObject, eventData.DamageValue));
        }
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}