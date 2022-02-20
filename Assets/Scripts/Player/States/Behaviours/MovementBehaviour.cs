using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    private static readonly int IsMove = Animator.StringToHash("isMove");
    public Joystick.Joystick Joystick { get; set; }

    [SerializeField] 
    private CharacteristicManager _characteristicManager;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Rigidbody _rigidbody;

    private float _speed;

    private bool _isMove;

    private void Start()
    {
        _speed = _characteristicManager.GetCharacteristicByType(CharacteristicType.Speed).GetMaxValue();
    }
    
    public void FixedUpdate()
    {
        if (Joystick == null)
        {
            return;
        }
        
        if (!_isMove)
        {
            _animator.SetBool(IsMove, false);
            return;
        }
        
        if (Joystick.Horizontal == 0 && Joystick.Vertical == 0)
        {
            _animator.SetBool(IsMove, false);
            return;
        }
        
        _animator.SetBool(IsMove, true);
        
        _rigidbody.velocity =
            new Vector3(-Joystick.Vertical * _speed, _rigidbody.velocity.y, Joystick.Horizontal * _speed);
        transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
    }
    
    public void StartMove()
    {
        _isMove = true;
    }
    
    public void StopMove()
    {
        _isMove = false;
    }
}