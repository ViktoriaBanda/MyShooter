using SimpleEventBus.Events;

public class PlayerTakesDamageEvent : EventBase
{
    public float DamageValue { get; }
    public PlayerTakesDamageEvent(float damageValue)
    {
        DamageValue = damageValue;
    }
}