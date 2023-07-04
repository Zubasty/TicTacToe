using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackToGameMenu : MonoBehaviour
{
    [SerializeField] private ClientObject _client;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnBackToGame);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnBackToGame);
    }

    private void OnBackToGame()
    {
        _client.BackToGame(int.Parse(_inputField.text));
    }
}
