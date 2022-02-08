using System.Collections;
using System.Collections.Generic;
using SimpleEventBus.Disposables;
using SimpleEventBus.Events;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _time;

    [SerializeField] 
    private float _maxTime = 45;
    
    private float _currentTime;
    
    private CompositeDisposable _subscriptions;
    private void Start()
    {
        StartCoroutine(StartTimer());
        _subscriptions = new CompositeDisposable
        { 
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler),
            EventStreams.Game.Subscribe<LevelWinEvent>(StopTimer),
            EventStreams.Game.Subscribe<PlayerDiedEvent>(StopTimer)
        };
    }

    private void StopTimer(EventBase eventData)
    {
        StopAllCoroutines();
    }

    private void GameStartEventHandler(GameStartEvent obj)
    {
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        _currentTime = _maxTime;
        while (_currentTime > 0)
        {
            _time.text = _currentTime.ToString();
            yield return new WaitForSecondsRealtime(1);
            _currentTime--;
        }
        _time.text = _currentTime.ToString();
        
        EventStreams.Game.Publish(new LevelWinEvent());
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
