using System;
using UnityEngine;

public class ShootingBehaviour : MonoBehaviour
{
    public event Action AllEnemiesHaveKilledEvent;
    
    [SerializeField] 
    private ShootingTargets _shootingTargets;
    
    private bool _isShoot;

    private void Update()
    {
        if (!_isShoot)
        {
            return;
        }
        
        if (_shootingTargets.GetEnemiesNumber() <= 0)
        {
            AllEnemiesHaveKilledEvent?.Invoke();
            return;
        }
                
        var nearestEnemy = _shootingTargets.FindNearestEnemy();
        transform.LookAt(nearestEnemy.transform);
        
        EventStreams.Game.Publish(new PlayerShootingEvent(nearestEnemy));
    }

    public void StartAutoShoot()
    {
        _isShoot = true;
    }
    
    public void StopAutoShoot()
    {
        _isShoot = false;
    }
}
