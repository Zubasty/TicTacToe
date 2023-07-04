using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntoToGameMenu : MonoBehaviour
{
    [SerializeField] private ClientObject _client;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnIntoToGame);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnIntoToGame);
    }

    private void OnIntoToGame()
    {
        _client.IntoToGame(int.Parse(_inputField.text));
    }
}
