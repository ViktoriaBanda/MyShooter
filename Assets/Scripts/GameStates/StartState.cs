using SimpleEventBus.Disposables;
using Unity.VisualScripting;
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
        var spawnPoint = FindObjectOfType<PlayerSpawnPoint>();
        var joystick = FindObjectOfType<Joystick.Joystick>();
        var camera = FindObjectOfType<CameraController>();
        var healthBar = FindObjectOfType<UIElementPositionController>();
        var bulletPool = FindObjectOfType<BulletPool>();
        var enemySpawner = FindObjectOfType<EnemySpawner>();
        
        var player = Instantiate(_selectedPlayerPrefab);
        InitializeSceneComponents(player, spawnPoint.transform.position, joystick, camera, healthBar, bulletPool, enemySpawner);
        
        _stateMachine.Enter<GameState>();
    }

    private void InitializeSceneComponents(GameObject player, Vector3 spawnPoint, Joystick.Joystick joystick, 
        CameraController camera, UIElementPositionController healthBar, BulletPool bulletPool, EnemySpawner enemySpawner)
    {
        _gameState.Player = player.GetComponent<Player>();
        _gameState.SpawnPoint = spawnPoint;

        enemySpawner.GetComponent<EnemySpawner>().Player = player;

        var movementBehaviour = player.GetComponent<MovementBehaviour>();
        movementBehaviour.Joystick = joystick;

        camera.GetComponent<CameraController>().PlayerTransform = player.transform;

        healthBar.GetComponent<UIElementPositionController>().UIElementRoot = player;

        var weapon = player.GetComponentInChildren<Weapon>();
        weapon.BulletPool = bulletPool.GetComponent<BulletPool>();
    }
}
