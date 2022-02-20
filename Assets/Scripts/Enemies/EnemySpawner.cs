using Pool;
using SimpleEventBus.Disposables;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Player { get; set; }

    [SerializeField] 
    private EnemyController _enemyControllerPrefab;

    [SerializeField]
    private int _poolSize = 50;

    [SerializeField] 
    private Transform[] _enemiesSpawnPoints;

    [SerializeField]
    private int _enemiesPerSpawnPoint = 15;

    [SerializeField]
    private int _randomRadius = 5;

    private CompositeDisposable _subscriptions;

    private MonoBehaviourPool<EnemyController> _enemyPool;

    private Transform _enemySpawnPoint;

    private void Start()
    {
        _enemyPool = new MonoBehaviourPool<EnemyController>(_enemyControllerPrefab, _enemySpawnPoint, _poolSize);
    
        ShowEnemies();
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler)
        };
    }

    private void ShowEnemies()
    {
        foreach (var enemySpawnPoint in _enemiesSpawnPoints)
        {
            for (var i = 0; i < _enemiesPerSpawnPoint; i++)
            {
                var randomPosition = Random.insideUnitCircle * _randomRadius;
                var enemy = _enemyPool.Take();

                enemy.Initialize(Player);
                
                var enemyPosition = enemySpawnPoint.position;
                enemyPosition.x += randomPosition.x;
                enemyPosition.z += randomPosition.y;
                enemy.transform.position = enemyPosition;
            }
        }
    }
    
    private void GameStartEventHandler(GameStartEvent eventData)
    {
        _enemyPool.ReleaseAll();
        ShowEnemies();
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
