using UnityEngine;

public class Weapon : MonoBehaviour
{
    public void SpawnBullet(Bullet bullet)
    {
        bullet.Rigidbody.useGravity = false;
        bullet.transform.position = transform.position;
        bullet.Rigidbody.velocity = transform.forward * bullet.Speed;
    }
}
