using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    private static readonly int IsMove = Animator.StringToHash("isMove");
 
    [SerializeField]
    private Animator _animator;
    
    [SerializeField] 
    private CharacteristicManager _characteristicManager;
    
    private float _speed;
    
    private bool _isMove;

    private void Start()
    {
        _speed = _characteristicManager.GetCharacteristicByName(GlobalConstants.SPEED).GetMaxValue();
    }

    private void Update()
    {
        if (!_isMove)
        {
            _animator.SetBool(IsMove, false);
            return;
        }
        
        _animator.SetBool(IsMove, true);
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.back * Time.deltaTime * _speed, Space.World);
            return;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _speed, Space.World);
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.left * Time.deltaTime * _speed, Space.World);
            return;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed, Space.World);
            return;
        }
        
        _animator.SetBool(IsMove, false);
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
