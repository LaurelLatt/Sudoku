using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellView : MonoBehaviour, IPointerClickHandler
{
    public Cell Cell { get; private set; }
    
    [SerializeField] private Image image;
    [SerializeField] private Text text;

    public void BindCell(Cell c) {
        Cell = c;
        Cell.OnValueChanged += SetText;
        Cell.OnInputIncorrect += SetTextToWrongColor;
        Cell.OnInputCorrect += SetTextToCorrectColor;
        
        SetText(Cell.DisplayedValue);
        SetInitialTextColor();
    }

    public void UnbindCell()
    {
        if (Cell != null)
        {
            Cell.OnValueChanged -= SetText;
            Cell.OnInputIncorrect -= SetTextToWrongColor;
            Cell.OnInputCorrect -= SetTextToCorrectColor;
            Cell = null;
        }
        SetText(0);
        
    }
    

    private void SetText(int value)
    {
        Debug.Log("OnValueChanged invoked");
        text.text = value == 0 ? "" : value.ToString();
    }

    private void SetInitialTextColor()
    {
        text.color = Cell.IsEditable == true ? Color.blue : Color.black;
    }

    public void SetTextToWrongColor(bool value)
    {
        if (BoardManager.Instance.MistakesOn)
        {
            text.color = Color.red;
        }
    }

    private void SetTextToCorrectColor()
    {
        text.color = Color.blue;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("CellView.OnPointerClick");
        BoardManager.Instance.SelectCell(this);
        HighlightCell();
    }

    public void HighlightCell()
    {
        Color highlightColor = Color.lightBlue;
        highlightColor.a = 0.7f;
        image.color = highlightColor;
    }

    public void UnhighlightCell()
    {
        image.color = Color.white;
    }
}
