
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private Vector3 _offset;
    
    public Transform PlayerTransform { get; set; }
    
    
    private void LateUpdate()
    {
        transform.position = PlayerTransform.position + _offset;
    }
}
