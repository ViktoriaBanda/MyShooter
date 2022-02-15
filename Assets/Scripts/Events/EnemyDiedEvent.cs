using Enemies;
using SimpleEventBus.Events;
using UnityEngine;

public class EnemyDiedEvent : EventBase
{
    public Zombie Enemy { get; }
    public EnemyDiedEvent(Zombie enemy)
    {
        Enemy = enemy;
    }
}