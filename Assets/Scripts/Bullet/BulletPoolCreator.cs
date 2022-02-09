using Pool;
using UnityEngine;

public class BulletPoolCreator : MonoBehaviour
{
    public MonoBehaviourPool<Bullet> BulletPool { get; private set; }
    
    [SerializeField] 
    private Bullet _bulletPrefab;
    
    [SerializeField]
    private int _poolSize = 30;
    
    private Transform _bulletSpawnPoint;
    
    private void Awake()
    {
        BulletPool = new MonoBehaviourPool<Bullet>(_bulletPrefab, _bulletSpawnPoint, _poolSize);
    }
}
