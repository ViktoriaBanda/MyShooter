using SimpleEventBus.Disposables;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] 
    private BulletPoolCreator _bulletPoolCreator;

    [SerializeField] 
    private Weapon weapon;
    
    private CompositeDisposable _subscriptions;
    
    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<PlayerShootingEvent>(PlayerShootingEventHandler),
            EventStreams.Game.Subscribe<BulletHitEvent>(BulletHitEventHandler)
        };
    }
    
    private void PlayerShootingEventHandler(PlayerShootingEvent eventData)
    {
        var bullet = _bulletPoolCreator.BulletPool.Take();
        weapon.SpawnBullet(bullet);
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
