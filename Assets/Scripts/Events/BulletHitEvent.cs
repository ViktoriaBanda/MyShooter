using SimpleEventBus.Events;
using UnityEngine;

public class BulletHitEvent : EventBase
{
    public Bullet Bullet { get; }
    public GameObject HittedObject { get; }
    
    public float DamageValue { get; set; }

    public BulletHitEvent(Bullet bullet, GameObject collisionHittedObject, float damageValue)
    {
        Bullet = bullet;
        HittedObject = collisionHittedObject;
        DamageValue = damageValue;
    }
}