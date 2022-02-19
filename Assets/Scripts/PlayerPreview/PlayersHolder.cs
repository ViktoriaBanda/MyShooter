using System.Collections;
using System.Collections.Generic;
using SimpleEventBus.Disposables;
using UnityEngine;

public class PlayersHolder : MonoBehaviour
{
    [SerializeField]
    private Transform _playerRoot;

    [SerializeField] 
    private PlayerSettingsProvider _playerSettingsProvider;

    private GameObject _player;
    private CompositeDisposable _subscriptions;

    private Dictionary<string, GameObject> _playerInstances = new();
    
    private void Awake()
    {
        var allPlayers = _playerSettingsProvider.GetAllPlayers();
        foreach (var playerSettings in allPlayers)
        {
            _playerInstances[playerSettings.Name] = Instantiate(playerSettings.Prefab, _playerRoot);
            _playerInstances[playerSettings.Name].SetActive(false);
        }
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<PlayerSelectionEvent>(PlayerSelectionEventHandler)
        };
    }

    public GameObject GetPlayer()
    {
        return _player;
    }
    
    private void PlayerSelectionEventHandler(PlayerSelectionEvent eventData)
    {
        if (_player != null)
        {
            _player.SetActive(false);
        }

        _player = _playerInstances[eventData.SelectedPlayer.Name];
        _player.transform.rotation = Quaternion.identity;
        _player.SetActive(true);
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}
