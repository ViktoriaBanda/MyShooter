using SimpleEventBus.Events;
using UnityEngine;

public class EnemyCollisionEvent : EventBase
{
    public GameObject GameObject { get; }

    public EnemyCollisionEvent(GameObject gameObject)
    {
        GameObject = gameObject;
    }
}