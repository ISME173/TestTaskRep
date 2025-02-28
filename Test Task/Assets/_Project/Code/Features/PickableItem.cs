using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(Outline))]
public class PickableItem : MonoBehaviour
{
    [SerializeField] private bool _activateOutlineInStart = true;

    private Rigidbody _rigidbody;
    private Collider _collider;
    private Outline _outline;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _outline = GetComponent<Outline>();

        _outline.enabled = _activateOutlineInStart;
    }

    private void Start() => Throw(Vector3.zero);

    public void PickUp(Transform holder)
    {
        _rigidbody.isKinematic = true;
        _collider.isTrigger = true;

        transform.SetParent(holder);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        _outline.enabled = false;
    }

    public void Throw(Vector3 force)
    {
        _rigidbody.isKinematic = false;
        _collider.isTrigger = false;

        transform.SetParent(null);

        _rigidbody.AddForce(force, ForceMode.Impulse);

        _outline.enabled = true;
    }
}
