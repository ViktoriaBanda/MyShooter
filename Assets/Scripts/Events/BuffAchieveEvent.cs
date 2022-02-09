using SimpleEventBus.Events;

public class BuffAchieveEvent : EventBase
{
    public Buff Buff { get; }
    public BuffAchieveEvent(Buff buff)
    {
        Buff = buff;
    }
}