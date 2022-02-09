using Pool;
using UnityEngine;

public class EnemyPoolCreator : MonoBehaviour
{
    public MonoBehaviourPool<EnemyController> EnemyPool { get; private set; }

    [SerializeField] 
    private EnemyController enemyControllerPrefab;
    
    [SerializeField]
    private int _poolSize = 50;
    
    private Transform _enemySpawnPoint;

    private void Awake()
    {
        EnemyPool = new MonoBehaviourPool<EnemyController>(enemyControllerPrefab, _enemySpawnPoint, _poolSize);
    }
}
