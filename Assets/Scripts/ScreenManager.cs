using System;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] 
    private Image _victory;
    
    [SerializeField] 
    private Image _gameOver;

    private CompositeDisposable _subscriptions;
    private void Awake()
    {
        HideScreens();
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<LevelWinEvent>(ShowVictoryScreen),
            EventStreams.Game.Subscribe<PlayerDiedEvent>(ShowGameOverScreen)
        };
    }
    
    public void HideScreens()
    {
        _victory.gameObject.SetActive(false);
        _gameOver.gameObject.SetActive(false);
        
        EventStreams.Game.Publish(new GameStartEvent());
    }
    
    private void ShowVictoryScreen(LevelWinEvent obj)
    {
        _victory.gameObject.SetActive(true);
    }
    
    private void ShowGameOverScreen(PlayerDiedEvent obj)
    {
        _gameOver.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
