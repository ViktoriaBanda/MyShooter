using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private Vector3 _offset;
    [SerializeField]
    private Transform _playerTransform;
    
    
    private void LateUpdate()
    {
        transform.position = _playerTransform.position + _offset;
    }
}
