
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform PlayerTransform { get; set; }

    [SerializeField] 
    private Vector3 _offset;


    private void LateUpdate()
    {
        transform.position = PlayerTransform.position + _offset;
    }
}
