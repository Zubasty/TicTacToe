using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public abstract class UserMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private Button _okButton;
    [SerializeField] private ClientObject _clientObject;
    [SerializeField] private MessageBox _message;

    public ClientObject Client => _clientObject;
    public TMP_InputField Name => _name;
    public TMP_InputField Password => _password;
    public Button OkButton => _okButton;
    public MessageBox Message => _message;

    protected virtual void OnEnable()
    {
        _okButton.onClick.AddListener(OnClick);
    }

    protected virtual void OnDisable()
    {
        _okButton.onClick.RemoveListener(OnClick);
    }

    protected virtual void OnClick()
    {
        gameObject.SetActive(false);
    }

    public void OnMainMenu() => _mainMenu.SetActive(true);
}
