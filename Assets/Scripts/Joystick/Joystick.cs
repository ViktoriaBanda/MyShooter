using UnityEngine;
using UnityEngine.EventSystems;

namespace Joystick
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public float Horizontal => _snapX ? SnapFloat(input.x, AxisOptions.Horizontal) : input.x;
        public float Vertical => _snapY ? SnapFloat(input.y, AxisOptions.Vertical) : input.y;

        public float HandleRange
        {
            set => _handleRange = Mathf.Abs(value);
        }

        public float DeadZone
        {
            set => _deadZone = Mathf.Abs(value);
        }

        [SerializeField] private float _handleRange = 1;
        [SerializeField] private float _deadZone = 0;
        [SerializeField] private AxisOptions _axisOptions = AxisOptions.Both;
        [SerializeField] private bool _snapX;
        [SerializeField] private bool _snapY;

        [SerializeField] protected RectTransform _background;
        [SerializeField] private RectTransform _handle;
    
        private Canvas _canvas;
        private Camera _camera;

        private Vector2 input = Vector2.zero;

        protected virtual void Start()
        {
            HandleRange = _handleRange;
            DeadZone = _deadZone;
            _canvas = GetComponentInParent<Canvas>();
            if (_canvas == null)
                Debug.LogError("The Joystick is not placed inside a canvas");

            Vector2 center = new Vector2(0.5f, 0.5f);
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
                _camera = _canvas.worldCamera;

            Vector2 position = RectTransformUtility.WorldToScreenPoint(_camera, _background.position);
            Vector2 radius = _background.sizeDelta / 2;
            input = (eventData.position - position) / (radius * _canvas.scaleFactor);
            FormatInput();
            HandleInput(input.magnitude, input.normalized);
            _handle.anchoredPosition = input * radius * _handleRange;
        }

        protected virtual void HandleInput(float magnitude, Vector2 normalised)
        {
            if (magnitude > _deadZone)
            {
                if (magnitude > 1)
                {
                    input = normalised;
                }
            }
            else
            {
                input = Vector2.zero;
            }
        }

        private void FormatInput()
        {
            switch (_axisOptions)
            {
                case AxisOptions.Horizontal:
                    input = new Vector2(input.x, 0f);
                    break;
                case AxisOptions.Vertical:
                    input = new Vector2(0f, input.y);
                    break;
            }
        }

        private float SnapFloat(float value, AxisOptions snapAxis)
        {
            if (value == 0)
            {
                return value;
            }

            if (_axisOptions != AxisOptions.Both)
            {
                return -1;
            }
            
            var angle = Vector2.Angle(input, Vector2.up);
                
            switch (snapAxis)
            {
                case AxisOptions.Horizontal when angle < 22.5f || angle > 157.5f:
                    return 0;
                case AxisOptions.Horizontal:
                    return value > 0 ? 1 : -1;
                case AxisOptions.Vertical when angle > 67.5f && angle < 112.5f:
                    return 0;
                case AxisOptions.Vertical:
                    return value > 0 ? 1 : -1;
                default:
                    return value;
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            input = Vector2.zero;
            _handle.anchoredPosition = Vector2.zero;
        }
    }
}