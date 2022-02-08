using SimpleEventBus.Events;
using UnityEngine;

public class EnemyDiedEvent : EventBase
{
    public GameObject Enemy { get; }
    public EnemyDiedEvent(GameObject enemy)
    {
        Enemy = enemy;
    }
}