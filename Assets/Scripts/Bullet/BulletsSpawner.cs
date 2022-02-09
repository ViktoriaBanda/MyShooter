using UnityEngine;

public class BulletsSpawner : MonoBehaviour
{
    [SerializeField] 
    private Player _player;
    
    private Transform _bulletSpawnPoint;

    public void SpawnBullet(Bullet bullet)
    {
        _bulletSpawnPoint = _player.Pistol;
        
        bullet.Rigidbody.useGravity = false;
        bullet.transform.position = _bulletSpawnPoint.position;
        bullet.Rigidbody.velocity = _player.transform.forward * bullet.Speed;
    }
}
