using UnityEngine;
using Zenject;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField, Min(0)] private float _sensitivity = 0.3f;
    [SerializeField, Min(0)] private float _maxYAngle = 80.0f;

    [Inject] private UIManager _uiManager;
    private float _rotationX = 0.0f;

    private void Awake()
    {
        if (_player == null)
            _player = GetComponentInParent<Transform>();

        _rotationX = _player.rotation.x;
    }

    private void Update()
    {
        float mouseX = 0;
        float mouseY = 0;

        if (_uiManager.UserTouchPanelToPlayerMove.IsPressed)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == _uiManager.UserTouchPanelToPlayerMove.FingerId)
                {
                    if (touch.phase == TouchPhase.Moved)
                    {
                        mouseY = touch.deltaPosition.y;
                        mouseX = touch.deltaPosition.x;
                    }
                    else if (touch.phase == TouchPhase.Stationary)
                    {
                        mouseY = 0;
                        mouseX = 0;
                    }
                }
            }
        }

        _player.Rotate(Vector3.up * mouseX * _sensitivity, Space.World);

        _rotationX -= mouseY * _sensitivity;
        _rotationX = Mathf.Clamp(_rotationX, -_maxYAngle, _maxYAngle);
        transform.localRotation = Quaternion.Euler(_rotationX, 0.0f, 0.0f);
    }
}
