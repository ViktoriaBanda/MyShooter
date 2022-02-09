using Pool;
using UnityEngine;

public class EnemyPoolCreator : MonoBehaviour
{
    public MonoBehaviourPool<Enemy> EnemyPool { get; private set; }

    [SerializeField] 
    private Enemy _enemyPrefab;
    
    [SerializeField]
    private int _poolSize = 50;
    
    private Transform _enemySpawnPoint;

    private void Awake()
    {
        EnemyPool = new MonoBehaviourPool<Enemy>(_enemyPrefab, _enemySpawnPoint, _poolSize);
    }
}
