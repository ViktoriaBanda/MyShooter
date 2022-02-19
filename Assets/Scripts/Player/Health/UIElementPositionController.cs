using UnityEngine;

public class UIElementPositionController : MonoBehaviour
{
    [SerializeField] 
    private RectTransform _uiElement;

    public GameObject UIElementRoot { get; set; }

    [SerializeField] 
    private Vector3 _offset;

    private void LateUpdate()
    {
        var pointInScreenSpace = Camera.main.WorldToScreenPoint(UIElementRoot.transform.position + _offset);
    
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, pointInScreenSpace,
            null, out var localPoint);

        _uiElement.anchoredPosition = localPoint;
    }
}
