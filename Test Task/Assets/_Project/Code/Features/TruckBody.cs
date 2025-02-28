using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TruckBody : MonoBehaviour
{
    [SerializeField, Min(0)] private int _pickedObjectsCountToWin;

    private Collider _collider;
    private int _pickedObjectsCountInTruckBody = 0;

    public event Action TruckBodyIsFull;
    public event Action<int> OnChangeCountPickableItemsInCount;

    private void Awake() => _collider = GetComponent<Collider>();

    private void Start()
    {
        _collider.isTrigger = true;

        if (_pickedObjectsCountToWin == 0)
        {
            _pickedObjectsCountToWin = FindObjectsOfType<PickableItem>().Length + 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PickableItem pickableItem))
        {
            _pickedObjectsCountInTruckBody++;
            OnChangeCountPickableItemsInCount?.Invoke(_pickedObjectsCountInTruckBody);

            if (_pickedObjectsCountInTruckBody >= _pickedObjectsCountToWin)
                TruckBodyIsFull?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PickableItem pickableItem))
        {
            _pickedObjectsCountInTruckBody--;
            OnChangeCountPickableItemsInCount?.Invoke(_pickedObjectsCountInTruckBody);
        }
    }
}
