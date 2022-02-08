using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private EnemyPoolCreator _enemyCreator;
    [SerializeField] 
    private Transform[] _enemiesSpawnPoints;
    [SerializeField]
    private int _enemiesPerSpawnPoint = 15;

    [SerializeField]
    private int _randomRadius = 5;

    [SerializeField] private GameObject _player;
    private void Start()
    {
        ShowEnemies();
    }
    
    private void ShowEnemies()
    {
        foreach (var enemySpawnPoint in _enemiesSpawnPoints)
        {
            for (var i = 0; i < _enemiesPerSpawnPoint; i++)
            {
                var randomPosition = Random.insideUnitCircle * _randomRadius;
                var enemy = _enemyCreator.EnemyPool.Take();

                enemy.Initialize(_player);
                
                var enemyPosition = enemySpawnPoint.position;
                enemyPosition.x += randomPosition.x;
                enemyPosition.z += randomPosition.y;
                enemy.transform.position = enemyPosition;
            }
        }
    }
}
