using Pool;
using UnityEngine;

public class EnemyPoolCreator : MonoBehaviour
{
    [SerializeField] 
    private Enemy _enemyPrefab;
    
    [SerializeField]
    private int _poolSize = 50;
    
    private Transform _enemySpawnPoint;
    
    public MonoBehaviourPool<Enemy> EnemyPool;
    
    private void Awake()
    {
        EnemyPool = new MonoBehaviourPool<Enemy>(_enemyPrefab, _enemySpawnPoint, _poolSize);
    }
}
