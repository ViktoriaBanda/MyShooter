using SimpleEventBus.Events;
using UnityEngine;

public class BuffAchieveEvent : EventBase
{
    public Buff Buff { get; }
    
    public BuffAchieveEvent(Buff buff)
    {
        Buff = buff;
    }
}