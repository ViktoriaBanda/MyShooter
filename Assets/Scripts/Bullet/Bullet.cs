using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody Rigidbody => _rigidbody;

    public float Speed => _speed;

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField] 
    private float _speed;

    private void OnCollisionEnter(Collision collision)
    {
        EventStreams.Game.Publish(new BulletHitEvent(this, collision.gameObject));
    }
}
