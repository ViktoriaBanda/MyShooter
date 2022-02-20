using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartState : MonoBehaviour, IState
{
    [SerializeField] 
    private GameState _gameState;
    
    [SerializeField] 
    private PlayerSettingsProvider _playerSettingsProvider;
    
    private GameObject _selectedPlayerPrefab;
    
    private StateMachine _stateMachine;

    private CompositeDisposable _subscriptions;
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<GameSceneLoadedEvent>(GameSceneLoadedEventHandler)
        };
        
        
        var lastSelectedPlayerName = PrefsManager.GetLastSelectedPlayer();
        var lastSelectedPlayer = _playerSettingsProvider.GetPlayer(lastSelectedPlayerName);
        _selectedPlayerPrefab = lastSelectedPlayer.Prefab;
        
        SceneManager.LoadScene(GlobalConstants.GAME_SCENE);
    }

    public void OnExit()
    {
        _subscriptions.Dispose();
    }
    
    private void GameSceneLoadedEventHandler(GameSceneLoadedEvent eventData)
    {
        var player = Instantiate(_selectedPlayerPrefab);
        
        FindAndInitializeGameObjects(player);
        
        _stateMachine.Enter<GameState>();
    }

    private void FindAndInitializeGameObjects(GameObject player)
    {
        var spawnPoint = FindObjectOfType<PlayerSpawnPoint>();
        var joystick = FindObjectOfType<Joystick.Joystick>();
        var camera = FindObjectOfType<CameraController>();
        var healthBar = FindObjectOfType<UIElementPositionController>();
        var bulletPool = FindObjectOfType<BulletPool>();
        var enemySpawner = FindObjectOfType<EnemySpawner>();

        InitializeSceneComponents(player, spawnPoint.transform.position, joystick, camera, healthBar, bulletPool,
            enemySpawner);
    }

    private void InitializeSceneComponents(GameObject player, Vector3 spawnPoint, Joystick.Joystick joystick, 
        CameraController camera, UIElementPositionController healthBar, BulletPool bulletPool, EnemySpawner enemySpawner)
    {
        _gameState.Player = player.GetComponent<Player>();
        _gameState.SpawnPoint = spawnPoint;

        enemySpawner.GetComponent<EnemySpawner>().Player = player;
        camera.GetComponent<CameraController>().PlayerTransform = player.transform;
        healthBar.GetComponent<UIElementPositionController>().UIElementRoot = player;
        
        var movementBehaviour = player.GetComponent<MovementBehaviour>();
        movementBehaviour.Joystick = joystick;

        var weapon = player.GetComponentInChildren<Weapon>();
        weapon.BulletPool = bulletPool.GetComponent<BulletPool>();
    }
}
