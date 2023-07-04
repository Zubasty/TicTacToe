using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellType
{
    Empty,
    Cross,
    Nought
}

public static class ExtensionCell
{
    public static string GetVisualString(this CellType cell)
    {
        switch (cell)
        {
            case CellType.Empty:
                return " ";
            case CellType.Cross:
                return "X";
            case CellType.Nought:
                return "O";
            default:
                throw new System.Exception("Неизвестный тип ошибки");
        }
    }
}