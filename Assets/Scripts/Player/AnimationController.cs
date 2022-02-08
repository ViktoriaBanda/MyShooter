using System.Collections;
using System.Collections.Generic;
using SimpleEventBus.Disposables;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private static readonly int IsMove = Animator.StringToHash("isMove");
    private static readonly int IsShoot = Animator.StringToHash("isShoot");
    private static readonly int IsDead = Animator.StringToHash("isDead");
    
    [SerializeField] 
    private GameObject _player;
    
    [SerializeField]
    private Animator _animator;
    
    private CompositeDisposable _subscriptions;
    
    private void Start()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler),
            EventStreams.Game.Subscribe<PlayerMovingEvent>(PlayerMovingEventHandler),
            EventStreams.Game.Subscribe<PlayerStoppedEvent>(PlayerStoppedEventHandler),
            EventStreams.Game.Subscribe<PlayerShootingEvent>(PlayerShootingEventHandler),
            EventStreams.Game.Subscribe<PlayerStopShootingEvent>(PlayerStopShootingEventHandler),
            EventStreams.Game.Subscribe<PlayerDiedEvent>(PlayerDiedEventHandler)
        };
    }
    
    private void GameStartEventHandler(GameStartEvent eventData)
    {
        _animator.SetBool(IsDead, false);
    }

    private void PlayerDiedEventHandler(PlayerDiedEvent eventData)
    {
        _animator.SetBool(IsDead, true);
    }

    private void PlayerStopShootingEventHandler(PlayerStopShootingEvent eventData)
    {
        _animator.SetBool(IsShoot, false);
    }

    private void PlayerShootingEventHandler(PlayerShootingEvent eventData)
    {
        _animator.SetBool(IsShoot, true);
    }

    private void PlayerStoppedEventHandler(PlayerStoppedEvent eventData)
    {
        _animator.SetBool(IsMove, false);
    }

    private void PlayerMovingEventHandler(PlayerMovingEvent eventData)
    {
        _animator.SetBool(IsMove, true);
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
