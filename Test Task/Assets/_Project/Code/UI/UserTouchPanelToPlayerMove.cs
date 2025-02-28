using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UserTouchPanelToPlayerMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image _touchPanel;

    public bool IsPressed { get; private set; } = false;
    public int FingerId { get; private set; }

    private void Awake()
    {
        _touchPanel = GetComponent<Image>();

        Color panelColor = _touchPanel.color;
        panelColor.a = 0;
        _touchPanel.color = panelColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == gameObject)
        {
            IsPressed = true;
            FingerId = eventData.pointerId;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
    }
}
