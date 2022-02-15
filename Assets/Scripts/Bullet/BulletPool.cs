using Pool;
using SimpleEventBus.Disposables;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public MonoBehaviourPool<Bullet> Pool { get; private set; }
    
    [SerializeField] 
    private Bullet _bulletPrefab;
    
    [SerializeField]
    private int _poolSize = 30;
    
    private CompositeDisposable _subscriptions;
    
    private Transform _bulletSpawnPoint;
    
    private void Awake()
    {
        Pool = new MonoBehaviourPool<Bullet>(_bulletPrefab, _bulletSpawnPoint, _poolSize);
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<BulletHitEvent>(BulletHitEventHandler)
        };
    }
    
    private void BulletHitEventHandler(BulletHitEvent eventData)
    {
        var bulletToRelease = eventData.Bullet;
        Pool.Release(bulletToRelease);
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
