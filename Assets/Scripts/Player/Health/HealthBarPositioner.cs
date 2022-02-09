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
        var pointInScreenSpace = Camera.main.WorldToScreenPoint(_healthBarRoot.transform.position + _offset);
    
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, pointInScreenSpace,
            null, out var localPoint);

        _healthBar.anchoredPosition = localPoint;
    }
}
