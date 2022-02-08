using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed => _speed;

    [SerializeField] 
    private float _speed;
    
    public Transform Pistol;
}
