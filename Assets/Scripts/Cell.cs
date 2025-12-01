using System;
using UnityEngine;

public class Cell
{
    public int Row { get; }
    public int Col {get; }
    public bool IsEditable {get; }
    public int Value {get; private set;}

    public event Action<int> OnValueChanged;

    public Cell(int row, int col, int value, bool isEditable)
    {
        Row = row;
        Col = col;
        Value = value;
        IsEditable = isEditable;
    }

    public void SetValue(int val)
    {
        if (!IsEditable) return;
        Debug.Log($"Set Value: {val}");
        Value = val;
        OnValueChanged?.Invoke(Value);
    }
}
