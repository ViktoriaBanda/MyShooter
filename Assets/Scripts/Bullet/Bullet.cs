using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody Rigidbody => _rigidbody;

    [SerializeField]
    private Rigidbody _rigidbody;

    private void OnCollisionEnter(Collision collision)
    {
        EventStreams.Game.Publish(new BulletHitEvent(this, collision.gameObject));
    }
}
