using System.Collections;
using System.Collections.Generic;
using SimpleEventBus.Disposables;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] 
    private PlayersHolder _playersHolder;
    
    private CompositeDisposable _subscriptions;
    private GameObject _player;

    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<RotatingPlayerEvent>(RotatingPlayerEventHandler)
        };
    }

    private void RotatingPlayerEventHandler(RotatingPlayerEvent eventData)
    {
        _player = _playersHolder.GetPlayer();
        
        if (_player != null)
        {
            _player.transform.Rotate(0, -eventData.RotationY, 0);
        }
    }

    private void OnDestroy()
    {
        _subscriptions?.Dispose();
    }
}
