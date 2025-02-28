using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _isWinText;
    [SerializeField] private TextMeshProUGUI _textCountPickableItmesCount;
    [Space]
    [SerializeField] private Button _throwButton;
    [SerializeField] private Button _restartButton;
    [Space]
    [SerializeField] private Joystick _joystickToPlayerMove;
    [SerializeField] private UserTouchPanelToPlayerMove _userTouchPanelToPlayerMove;

    [Inject] private TruckBody _truckBody;

    public Button ThrowButton => _throwButton;
    public Joystick JoystickToPlayerMove => _joystickToPlayerMove;
    public UserTouchPanelToPlayerMove UserTouchPanelToPlayerMove => _userTouchPanelToPlayerMove;

    private void Awake()
    {
        _truckBody.TruckBodyIsFull += ShowIsWinText;
        _restartButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));

        _truckBody.OnChangeCountPickableItemsInCount += (int pickbleItmesCount) =>
        {
            _textCountPickableItmesCount.text = $"Pickable items in truck body: {pickbleItmesCount}";
        };
    }

    private void Start()
    {
        _restartButton.gameObject.SetActive(false);
    }

    private void ShowIsWinText()
    {
        _isWinText.text = "You win!";
        _isWinText.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
    }
}
