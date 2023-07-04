using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Button))]
public class Cell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private CellType _type;
    private RectTransform _rectTransform;
    private Button _button;
    private ClientObject _client;

    public event Action<string> MovedStep;

    public int X { get; private set; }
    public int Y { get; private set; }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(MoveStep);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(MoveStep);
    }

    public void Init(int x, int y, Vector2 position, ClientObject client)
    {
        X = x;
        Y = y;
        _rectTransform.anchoredPosition = position;
        _client = client;
    }

    public void TakeValue(string value)
    {
        _type = StringToCellType(value);
        _text.text = _type.GetVisualString();
    }

    private void MoveStep()
    {
        MovedStep?.Invoke(_client.Step(X, Y));
    }

    private CellType StringToCellType(string value)
    {
        switch(value)
        {
            case "E":
                return CellType.Empty;
            case "C":
                return CellType.Cross;
            case "N":
                return CellType.Nought;
            default:
                throw new System.Exception("Неизвестный тип ячейки");
        }
    }
}
