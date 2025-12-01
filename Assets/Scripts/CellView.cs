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
        
        Debug.Log($"Cell Bind - row: {Cell.Row} col: {Cell.Col} value: {Cell.Value} editable: {Cell.IsEditable}");
        SetText(Cell.Value);
        SetInitialTextColor();
    }

    private void OnDestroy()
    {
        Cell.OnValueChanged -= SetText;
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

    private void SetTextToWrongColor()
    {
        text.color = Color.red;
    }

    private void SetTextToCorrectColor()
    {
        text.color = Color.blue;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("CellView.OnPointerClick");
        BoardManager.Instance.SelectCell(this);
    }
}
