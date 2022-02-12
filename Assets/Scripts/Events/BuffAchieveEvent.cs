using SimpleEventBus.Events;
using UnityEngine;

public class BuffAchieveEvent : EventBase
{
    public Buff Buff { get; }
    //public AudioSource AudioSource;
    public BuffAchieveEvent(Buff buff)
    {
        Buff = buff;
        //AudioSource = audioSource;
    }
}