using System.Collections;
using System.Collections.Generic;
using Pool;
using SimpleEventBus.Disposables;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] 
    private Player _player;
    
    [SerializeField] 
    private Bullet _bulletPrefab;
    
    [SerializeField]
    private float _bulletSpeed;
    
    [SerializeField]
    private int _poolSize = 30;

    private Transform _bulletSpawnPoint;
    
    private MonoBehaviourPool<Bullet> _bulletPool;
    private CompositeDisposable _subscriptions;
    
    private void Awake()
    {
        _bulletSpawnPoint = _player.Pistol;
        
        _bulletPool = new MonoBehaviourPool<Bullet>(_bulletPrefab, _bulletSpawnPoint, _poolSize);
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<PlayerShootingEvent>(PlayerShootingEventHandler),
            EventStreams.Game.Subscribe<BulletHitEvent>(BulletHitEventHandler)
        };
    }

    private void BulletHitEventHandler(BulletHitEvent eventData)
    {
        var bulletToRelease = eventData.Bullet;
        _bulletPool.Release(bulletToRelease);
    }

    private void PlayerShootingEventHandler(PlayerShootingEvent eventData)
    {
        var bullet = _bulletPool.Take();
        bullet.Rigidbody.useGravity = false;
        bullet.transform.position = _bulletSpawnPoint.position;
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            var direction = hit.point - _bulletSpawnPoint.position;
            bullet.Rigidbody.velocity = direction.normalized * _bulletSpeed;
        }
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
