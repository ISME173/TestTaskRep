using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class PlayerMoving : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    [Inject] private UIManager _uiManager;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float horizonalMoving = _uiManager.JoystickToPlayerMove.Horizontal * _speed;
        float verticalMoving = _uiManager.JoystickToPlayerMove.Vertical * _speed;

        Vector3 movingDirection = transform.forward * verticalMoving + transform.right * horizonalMoving;
        _rigidbody.velocity = movingDirection * _speed;
    }
}
