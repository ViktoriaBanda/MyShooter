using System.Collections;
using System.Collections.Generic;
using SimpleEventBus.Disposables;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    [SerializeField] 
    private CharacteristicManager _characteristicManager;

    private float _playingAudioTime = 0.4f;
    
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
        StartCoroutine(PlaySoundAndHideBuff(eventData));
        
        _buffs.Add(eventData.Buff.gameObject);

        if(eventData.Buff.GetType() == typeof(Bomb))
        {
            return;    
        }
        
        _characteristicManager.GetCharacteristicByName(eventData.Buff.Name).SetValue
                (_characteristicManager.GetCharacteristicByName(eventData.Buff.Name).GetMaxValue());
    }

    private IEnumerator PlaySoundAndHideBuff(BuffAchieveEvent eventData)
    {
        if (eventData.AudioSource != null)
        {
            eventData.AudioSource.Play();
            yield return new WaitForSeconds(_playingAudioTime);
        }

        eventData.Buff.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
