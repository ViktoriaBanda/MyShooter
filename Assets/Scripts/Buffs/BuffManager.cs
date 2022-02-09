using SimpleEventBus.Disposables;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    [SerializeField] 
    private Player _player;
    
    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<BuffAchieveEvent>(BuffAchieveEventHandler)
        };
    }

    private void BuffAchieveEventHandler(BuffAchieveEvent eventData)
    {
        if (eventData.Buff.tag == "Medicine")
        {
            _player.SetHealth(_player.GetMaxHealth());
        }
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
