using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVisual : MonoBehaviour
{
    [SerializeField] private Cell _cellTemplate;
    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Transform _parent;
    [SerializeField] private ClientObject _client;
    [SerializeField] private int _size;

    private Cell[,] _cells;

    private void Start()
    {
        _cells = new Cell[_size, _size];

        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                _cells[i, j] = Instantiate(_cellTemplate, _parent);
                _cells[i, j].Init(i, j, _startPosition + new Vector2(_offset.x * i, _offset.y * j), _client);
                _cells[i, j].MovedStep += ParseStringToGame;
            }
        }

        StartCoroutine(UpdateStateGame());
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                _cells[i, j].MovedStep -= ParseStringToGame;
            }
        }
    }

    private IEnumerator UpdateStateGame()
    {
        while (true)
        {
            if (_client.IsInGame)
                ParseStringToGame(_client.GetGame());

            yield return new WaitForSeconds(3);
        }
    }

    private void ParseStringToGame(string gameString)
    {
        string[] info = gameString.Split('S');

        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                _cells[i, j].TakeValue(info[i+1][j].ToString());
            }
        }
    }
}
