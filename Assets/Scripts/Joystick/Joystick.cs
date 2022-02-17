using UnityEngine;
using UnityEngine.EventSystems;

namespace Joystick
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public float Horizontal => _input.x;
        public float Vertical => _input.y;

        [SerializeField] protected RectTransform _background;
        [SerializeField] private RectTransform _handle;
    
        private Canvas _canvas;
        private Camera _camera;

        private Vector2 _input = Vector2.zero;

        protected virtual void Start()
        {
            _canvas = GetComponentInParent<Canvas>();
            if (_canvas == null)
                Debug.LogError("The Joystick is not placed inside a canvas");

            var center = new Vector2(0.5f, 0.5f);
            _background.pivot = center;
            _handle.anchorMin = center;
            _handle.anchorMax = center;
            _handle.pivot = center;
            _handle.anchoredPosition = Vector2.zero;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _camera = null;
            if (_canvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                 _camera = _canvas.worldCamera;
            }

            var position = RectTransformUtility.WorldToScreenPoint(_camera, _background.position);
            var radius = _background.sizeDelta / 2;
            
            _input = (eventData.position - position) / (radius * _canvas.scaleFactor);

            if (_input.magnitude > 1)
            {
                _input = _input.normalized;
            }
            
            _handle.anchoredPosition = _input * radius;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            _input = Vector2.zero;
            _handle.anchoredPosition = Vector2.zero;
        }
    }
}