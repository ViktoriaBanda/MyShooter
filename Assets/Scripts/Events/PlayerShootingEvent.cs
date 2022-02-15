using Enemies;
using SimpleEventBus.Events;

public class PlayerShootingEvent : EventBase
{
    public Zombie Enemy { get; }
    public PlayerShootingEvent(Zombie enemy)
    {
        Enemy = enemy;
    }
}