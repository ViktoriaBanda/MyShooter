using SimpleEventBus.Events;
using UnityEngine;

public class EnemyTakesDamageEvent : EventBase
{
    public GameObject Enemy { get; }
    public float DamageValue { get; set; }

    public EnemyTakesDamageEvent(GameObject enemy, float damageValue)
    {
        Enemy = enemy;
        DamageValue = damageValue;
    }
}