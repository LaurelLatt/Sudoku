using System;
using UnityEngine;

public class Cell
{
    public int Row { get; }
    public int Col {get; }
    public bool IsEditable {get; }
    public int DisplayedValue {get; private set;}

    private int correctValue;

    public event Action<int> OnValueChanged;
    public event Action OnInputCorrect;
    public event Action OnInputIncorrect;
    

    public Cell(int row, int col, int displayedValue, bool isEditable, int correctValue)
    {
        Row = row;
        Col = col;
        DisplayedValue = displayedValue;
        IsEditable = isEditable;
        this.correctValue = correctValue;
    }

    public void SetValue(int val, bool notify = true)
    {
        if (!IsEditable) return;
        Debug.Log($"Set Value: {val}");

        DisplayedValue = val;
        OnValueChanged?.Invoke(DisplayedValue);

        BoardManager.Instance.CurrentBoard[Row][Col] = val;
        
        if (notify)
        {
            CheckInputForCorrect(val);
        }
    }

    private void CheckInputForCorrect(int val)
    {
        if (val == correctValue)
            OnInputCorrect?.Invoke();
        else
            OnInputIncorrect?.Invoke();
    }
}
