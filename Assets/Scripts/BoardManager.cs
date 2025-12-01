using System.Collections.Generic;
using Commands;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; private set; }
    [SerializeField] private List<CellView> cellViewList = new List<CellView>(81);
    
    [SerializeField] private PuzzleGenerator puzzleGenerator;
    
    private Cell selectedCellModel;
    private CellView selectedCellView;
    
    private CellView[,] cellViews = new CellView[9,9];
    
    private int[,] board;
    
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

    public void SetBoard()
    {
        TransferListToArray();
        board = puzzleGenerator.GeneratePuzzle();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                int value = board[i, j];
                CreateCell(i, j, value, value == 0);
            }
        }
    }

    private void CreateCell(int row, int column, int value, bool isEditable = true)
    {
        Cell cell = new Cell(row, column, value, isEditable);
        CellView cellView = cellViews[row, column];
        
        cellView.BindCell(cell);
    }

    private void TransferListToArray()
    {
        int index = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                cellViews[i,j] = cellViewList[index];
                index++;
            }
        }
    }
}
