using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Transform _handHolder;
    [Space]
    [SerializeField] private float _pickupDistance = 3f;
    [SerializeField] private float _throwForce = 10f;

    [Inject] private UIManager _uiManager;
    private PickableItem _currentItem;

    private void Start()
    {
        _uiManager.ThrowButton.gameObject.SetActive(false);
        _uiManager.ThrowButton.onClick.AddListener(ThrowCurrentItem);
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            TryPickupItem();
        }
    }

    private void TryPickupItem()
    {
        if (_currentItem != null) return;

        Ray ray = _playerCamera.ScreenPointToRay(Input.GetTouch(0).position);

        if (Physics.Raycast(ray, out RaycastHit hit, _pickupDistance))
        {
            if (hit.collider.TryGetComponent(out PickableItem item))
            {
                if (_currentItem != null) return;

                _currentItem = item;
                item.PickUp(_handHolder);
                _uiManager.ThrowButton.gameObject.SetActive(true);
            }
        }
    }

    private void ThrowCurrentItem()
    {
        if (_currentItem == null) return;

        Vector3 throwDirection = _playerCamera.transform.forward;
        _currentItem.Throw(throwDirection * _throwForce);
        _currentItem = null;
        _uiManager.ThrowButton.gameObject.SetActive(false);
    }
}
