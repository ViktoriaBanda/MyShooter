using SimpleEventBus.Events;
using UnityEngine;

public class BulletHitEvent : EventBase
{
    public Bullet Bullet { get; }
    public GameObject GameObject { get; }

    public BulletHitEvent(Bullet bullet, GameObject collisionGameObject)
    {
        Bullet = bullet;
        GameObject = collisionGameObject;
    }
}