using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LogInMenu : UserMenu
{
    [SerializeField] private GameMenu _gameMenu;

    private void Start()
    {
        Client.Loged += OnLoged;
        Client.LogFailed += OnLogFailed;
    }

    private void OnDestroy()
    {
        Client.Loged -= OnLoged;
        Client.LogFailed -= OnLogFailed;
    }

    protected override void OnClick()
    {
        base.OnClick();
        Client.LogIn(Name.text, Password.text);
    }

    private void OnLoged(Client client)
    {
        Message.gameObject.SetActive(true);
        Message.TakeTextMessage($"������������ {client.User.Name} �����������");
        Message.TakeTextButton("��");
        _gameMenu.gameObject.SetActive(true);
    }

    private void OnLogFailed(Client client)
    {
        Message.gameObject.SetActive(true);
        Message.TakeTextMessage($"������. ������������ {client.User.Name} �� " +
            $"���� ��������������. ��������� ������������ ����� ������ � ������");
        Message.TakeTextButton("��");
    }
}
