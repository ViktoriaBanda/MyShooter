using UnityEngine;

public class JoystickPlayer : MonoBehaviour
{
    private static readonly int IsMove = Animator.StringToHash("isMove");
    
    [SerializeField]
    private Animator _animator;
    
    public float _speed;
    public FixedJoystick _joystick;
    public Rigidbody _rigidbody;

    public void FixedUpdate()
    {
        if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
        {
            _animator.SetBool(IsMove, false);
            return;
        }
        
        _animator.SetBool(IsMove, true);
        
        _rigidbody.velocity =
            new Vector3(-_joystick.Vertical * _speed, _rigidbody.velocity.y, _joystick.Horizontal * _speed);
        transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
    }
}