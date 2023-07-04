using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _buttonText;

    public void TakeTextMessage(string text)
    {
        _text.text = text;
    }

    public void TakeTextButton(string text)
    {
        _buttonText.text = text;
    }
}
