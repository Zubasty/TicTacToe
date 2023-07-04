using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SignInMenu : UserMenu
{
    private void Start()
    {
        Client.Signed += OnSigned;
        Client.SignFailed += OnSignFalled;
    }

    private void OnDestroy()
    {
        Client.Signed -= OnSigned;
        Client.SignFailed -= OnSignFalled;
    }

    protected override void OnClick()
    {
        base.OnClick();
        OnMainMenu();
        Client.SignIn(Name.text, Password.text);
    }

    private void OnSigned(Client client)
    {
        Message.gameObject.SetActive(true);
        Message.TakeTextMessage($"Пользователь {client.User.Name} зарегистрирован");
        Message.TakeTextButton("Ок");
    }

    private void OnSignFalled(Client client)
    {
        Message.gameObject.SetActive(true);
        Message.TakeTextMessage($"Ошибка. Пользователь {client.User.Name} уже зарегистрирован");
        Message.TakeTextButton("Ок");
    }
}
