using SimpleEventBus.Disposables;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] 
    private AudioClip _audioClip;
    
    [SerializeField] 
    private BulletPoolCreator _bulletPoolCreator;
    
    [SerializeField]
    private float _reloadTimer = 0.2f;

    private float _currentTimer = 0;

    private bool _isTimerOver = true;

    private Bullet _currentBullet;

    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<PlayerShootingEvent>(PlayerShootingEventHandler)
        };
    }
    
    private void Update()
    {
        if (_currentTimer > 0)
        {
            _currentTimer -= Time.deltaTime;
            return;
        }

        _isTimerOver = true;
    }

    private void PlayerShootingEventHandler(PlayerShootingEvent eventData)
    {
        if (_isTimerOver)
        {
            _currentBullet = _bulletPoolCreator.BulletPool.Take();
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            Shoot();
            
            _currentTimer = _reloadTimer;
            _isTimerOver = false;
        }
    }
    
    private void Shoot()
    {
        _currentBullet.Rigidbody.useGravity = false;
        _currentBullet.transform.position = transform.position;
        _currentBullet.transform.rotation = transform.rotation;
        _currentBullet.Rigidbody.velocity = transform.forward * _currentBullet.Speed;
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
