 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private ClientObject _client;
    [SerializeField] private Button _buttonNewGameCross;
    [SerializeField] private Button _buttonNewGameNought;
    [SerializeField] private Button _buttonIntoToGame;

    private void OnEnable()
    {
        _buttonNewGameCross.onClick.AddListener(AddGameCross);
        _buttonNewGameNought.onClick.AddListener(AddGameNought);
    }

    private void OnDisable()
    {
        _buttonNewGameCross.onClick.RemoveListener(AddGameCross);
        _buttonNewGameNought.onClick.RemoveListener(AddGameNought);
    }

    private void AddGameCross()
    {
        _client.AddGame("C");
    }

    private void AddGameNought()
    {
        _client.AddGame("N");
    }
}
