using System.Collections.Generic;
using SimpleEventBus.Disposables;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    [SerializeField] 
    private CharacteristicManager _characteristicManager;

    private List<GameObject> _buffs;
    
    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _buffs = new List<GameObject>();
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.Game.Subscribe<BuffAchieveEvent>(BuffAchieveEventHandler),
            EventStreams.Game.Subscribe<GameStartEvent>(GameStartEventHandler)
        };
    }

    private void GameStartEventHandler(GameStartEvent eventData)
    {
        foreach (var buff in _buffs)
        {
            buff.SetActive(true);
        }
    }

    private void BuffAchieveEventHandler(BuffAchieveEvent eventData)
    {
        eventData.Buff.gameObject.SetActive(false);
        
        _buffs.Add(eventData.Buff.gameObject);

        if(eventData.Buff.GetType() == typeof(Bomb))
        {
            return;    
        }
        
        _characteristicManager.GetCharacteristicByType(eventData.Buff.Type).SetValue
                (_characteristicManager.GetCharacteristicByType(eventData.Buff.Type).GetMaxValue());
    }


    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
