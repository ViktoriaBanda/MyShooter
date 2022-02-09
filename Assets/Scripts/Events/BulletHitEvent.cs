using SimpleEventBus.Events;
using UnityEngine;

public class BulletHitEvent : EventBase
{
    public Bullet Bullet { get; }
    public GameObject HittedObject { get; }

    public BulletHitEvent(Bullet bullet, GameObject collisionHittedObject)
    {
        Bullet = bullet;
        HittedObject = collisionHittedObject;
    }
}