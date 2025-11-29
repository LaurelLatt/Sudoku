using Commands;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; private set; }
    
    private Cell selectedCellModel;
    private CellView selectedCellView;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    private void Update() {
        if (selectedCellModel == null) return;

        for (int i = 1; i <= 9; i++) {
            if (Input.GetKeyDown(i.ToString())) {
                PlaceNumber(i);
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace)) {
            PlaceNumber(0);
        }
    }
    
    public void SelectCell(CellView view) {
        selectedCellView = view;
        selectedCellModel = view.cell;  // gets the model
    }

    private void PlaceNumber(int number) {
        if (!selectedCellModel.IsEditable)
            return;

        Command cmd = new SetNumberCommand(selectedCellModel, number);
        CommandManager.Instance.Execute(cmd);
    }
}
