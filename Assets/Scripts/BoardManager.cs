using System;
using System.Collections.Generic;
using Commands;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; private set; }
    public int MistakeCount { get; private set; }
    
    [SerializeField] private List<CellView> cellViewList = new List<CellView>(81);
    
    [SerializeField] private PuzzleGenerator puzzleGenerator;
    
    private Cell selectedCellModel;
    private CellView selectedCellView;
    
    private CellView[,] cellViews = new CellView[9,9];
    
    private int[,] puzzleBoard;
    private int[,] solutionBoard;

    public event Action<int> OnMistakeCountChanged;
    
    
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
                Debug.Log($"Input received: {i}");
                PlaceNumber(i);
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace)) {
            PlaceNumber(0);
        }
    }
    
    private void RegisterCell(Cell cell)
    {
        cell.OnInputIncorrect += HandleMistake;
    }
    
    
    public void SelectCell(CellView view) {
        
        if (selectedCellModel != null)
        {
            // set old cell view back to normal
            selectedCellView.UnhighlightCell();
        }

        // set new selected cell
        selectedCellView = view;
        selectedCellModel = view.Cell; // gets the model
        Debug.Log($"Selected cell: {selectedCellModel.Row}, {selectedCellModel.Col}, editable: {selectedCellModel.IsEditable}");
    }

    private void PlaceNumber(int number) {
        if (!selectedCellModel.IsEditable)
        {
            Debug.Log("Cell is not editable");
            return;
        }

        Command cmd = new SetNumberCommand(selectedCellModel, number);
        CommandManager.Instance.Execute(cmd);
    }

    public void SetBoard()
    {
        TransferListToArray();
        puzzleBoard = puzzleGenerator.GeneratePuzzle();
        solutionBoard = puzzleGenerator.solvedPuzzle;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                int displayValue = puzzleBoard[i, j];
                int correctValue = solutionBoard[i, j];
                bool isEditable = displayValue == 0;
                CreateCell(i, j, displayValue, isEditable, correctValue);
            }
        }
    }

    private void CreateCell(int row, int column, int displayValue, bool isEditable, int correctValue)
    {
        Cell cell = new Cell(row, column, displayValue, isEditable, correctValue);
        CellView cellView = cellViews[row, column];
        
        cellView.BindCell(cell);
        
        RegisterCell(cell);
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
    
    private void HandleMistake()
    {
        MistakeCount++;
        OnMistakeCountChanged?.Invoke(MistakeCount);
    }
}
