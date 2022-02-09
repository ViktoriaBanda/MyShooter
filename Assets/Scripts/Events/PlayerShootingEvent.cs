using SimpleEventBus.Events;
using UnityEngine;

public class PlayerShootingEvent : EventBase
{
    public GameObject Enemy { get; }
    public PlayerShootingEvent(GameObject enemy)
    {
        Enemy = enemy;
    }
}