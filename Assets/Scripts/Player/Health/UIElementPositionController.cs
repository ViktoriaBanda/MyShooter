using UnityEngine;

public class UIElementPositionController : MonoBehaviour
{
    [SerializeField] 
    private RectTransform _uiElement;

    [SerializeField] 
    private GameObject _uiElementRoot;

    [SerializeField] 
    private Vector3 _offset;

    private void LateUpdate()
    {
        var pointInScreenSpace = Camera.main.WorldToScreenPoint(_uiElementRoot.transform.position + _offset);
    
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, pointInScreenSpace,
            null, out var localPoint);

        _uiElement.anchoredPosition = localPoint;
    }
}
