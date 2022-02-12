using SimpleEventBus.Events;
using UnityEngine;

public class EnemyTakesDamageEvent : EventBase
{
    public GameObject Enemy { get; }
    public EnemyTakesDamageEvent(GameObject enemy)
    {
        Enemy = enemy;
    }
}