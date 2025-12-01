using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellView : MonoBehaviour, IPointerClickHandler
{
    public Cell cell { get; private set; }
    
    [SerializeField] private Image image;
    [SerializeField] private Text text;

    public void BindCell(Cell c) {
        cell = c;
        cell.OnValueChanged += SetText;
        
        SetText(cell.Value);
        SetInitialTextColor();
    }

    private void OnDestroy()
    {
        cell.OnValueChanged -= SetText;
    }

    private void SetText(int value)
    {
        text.text = value == 0 ? "" : value.ToString();
    }

    private void SetInitialTextColor()
    {
        text.color = cell.IsEditable == true ? Color.blue : Color.black;
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
        BoardManager.Instance.SelectCell(this);
    }
}
