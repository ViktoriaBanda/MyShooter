using System;
using SimpleEventBus.Disposables;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public event Action OnShooting;

    public BulletPool BulletPool { get; set; }
    
    [SerializeField] private AudioClip _audioClip;

    [SerializeField] private float _reloadTimer = 0.2f;

    private float _currentTimer = 0;

    private bool _isTimerOver = true;

    private Bullet _currentBullet;

    private void Update()
    {
        if (_currentTimer > 0)
        {
            _currentTimer -= Time.deltaTime;
            return;
        }

        _isTimerOver = true;
    }

    public void StartShooting()
    {
        if (_isTimerOver)
        {
            _currentBullet = BulletPool.Pool.Take();
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            Shoot();

            _currentTimer = _reloadTimer;
            _isTimerOver = false;
        }
    }

    private void Shoot()
    {
        OnShooting?.Invoke();

        _currentBullet.Rigidbody.useGravity = false;
        _currentBullet.transform.position = transform.position;
        _currentBullet.transform.rotation = transform.rotation;
        _currentBullet.Rigidbody.velocity = transform.forward * _currentBullet.Speed;
    }
}

    
