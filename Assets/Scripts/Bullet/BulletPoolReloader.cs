using SimpleEventBus.Disposables;
using UnityEngine;

public class BulletPoolReloader : MonoBehaviour
{
    [SerializeField] 
    private BulletPoolCreator _bulletPoolCreator;

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
        var bulletToRelease = eventData.Bullet;
        _bulletPoolCreator.BulletPool.Release(bulletToRelease);
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
