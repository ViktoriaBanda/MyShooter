using UnityEngine;

public class HealthBarPositioner : MonoBehaviour
{
    [SerializeField] 
    private RectTransform _healthBar;

    [SerializeField] 
    private GameObject _healthBarRoot;

    [SerializeField] 
    private Vector3 _offset;

    private void LateUpdate()
    {
        // World Space Point To Screen Space Point
        var pointInScreenSpace = Camera.main.WorldToScreenPoint(_healthBarRoot.transform.position + _offset);
    
        // Screen Space Point to Canvas Space Point
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, pointInScreenSpace,
            null, out var localPoint);

        _healthBar.anchoredPosition = localPoint;
    }
}
