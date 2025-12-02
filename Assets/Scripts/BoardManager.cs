using System;
using System.Collections.Generic;
using Commands;
using States;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public event Action<int> OnMistakeCountChanged;
    public static BoardManager Instance { get; private set; }
    public int[,] CurrentBoard = new int[9,9];
    
    [SerializeField] private List<CellView> cellViewList = new List<CellView>(81);
    [SerializeField] private PuzzleGenerator puzzleGenerator;
    
    private Cell selectedCellModel;
    private CellView selectedCellView;
    private CellView[,] cellViews = new CellView[9,9];
    private Cell[,] cells = new Cell[9,9];

    private int[,] puzzleTemplate;
    private int[,] solutionBoard;
    
    public int MistakeCount { get; private set; }
    
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
            RemoveNumber();
        }
    }

    public bool CheckPuzzleComplete()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (CurrentBoard[i, j] != solutionBoard[i, j])
                {
                    return false;
                }
            }
        }

        return true;
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

    private void RemoveNumber()
    {
        if (!selectedCellModel.IsEditable)
        {
            Debug.Log("Cell is not editable");
            return;
        }

        Command cmd = new DeleteCommand(selectedCellModel);
        CommandManager.Instance.Execute(cmd);
    }

    public void SetNewBoard()
    {
        PopulateGrids();
        CreateCells();
    }

    public void SetBoardFromLoad()
    {
        if (CurrentBoard == null || solutionBoard == null || puzzleTemplate == null) return;
        CreateCells();
    }

    private void CreateCells()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                int displayValue = CurrentBoard[i, j];
                int correctValue = solutionBoard[i, j];
                bool isEditable = puzzleTemplate[i, j] == 0;
                CreateCell(i, j, displayValue, isEditable, correctValue);
            }
        }
    }

    public void ClearBoard()
    {
        for (int r = 0; r < 9; r++)
        {
            for (int c = 0; c < 9; c++)
            {
                // 1. Unregister model events (if model exists)
                if (cells[r, c] != null)
                {
                    UnregisterCell(cells[r, c]);
                    cells[r, c] = null;
                }

                // 2. Unbind cell views from cells
                if (cellViews[r, c] != null)
                {
                    cellViews[r, c].UnbindCell();
                }
            }
        }
    }


    private void CreateCell(int row, int column, int displayValue, bool isEditable, int correctValue)
    {
        Cell cell = new Cell(row, column, displayValue, isEditable, correctValue);
        CellView cellView = cellViews[row, column];
        
        cellView.BindCell(cell);
        cells[row, column] = cell;
        RegisterCell(cell);
    }
    
    private void RegisterCell(Cell cell)
    {
        cell.OnInputIncorrect += HandleMistake;
        cell.OnInputCorrect += HandleCorrectInput;
    }
    private void UnregisterCell(Cell cell)
    {
        cell.OnInputIncorrect -= HandleMistake;
        cell.OnInputCorrect -= HandleCorrectInput;
    }

    private void TransferCellViewListToArray()
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
    
    private void PopulateGrids()
    {
        TransferCellViewListToArray();
        puzzleTemplate = puzzleGenerator.GeneratePuzzle();
        solutionBoard = puzzleGenerator.solvedPuzzle;
        CurrentBoard = ArrayFunctions.Copy2DArray(puzzleTemplate);
    }

    public void LoadSavedGrids(int[,] puzzleTemplate, int[,] solutionBoard, int[,] currentBoard)
    {
        TransferCellViewListToArray();
        this.puzzleTemplate = puzzleTemplate;
        this.solutionBoard = solutionBoard;
        CurrentBoard = currentBoard;
    }
    
    private void HandleMistake()
    {
        MistakeCount++;
        OnMistakeCountChanged?.Invoke(MistakeCount);
    }

    private void HandleCorrectInput()
    {
        if (CheckPuzzleComplete())
        {
            StateManager.Instance.ChangeToResultsState();
        }
    }

    public void CompletePuzzle()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Cell cell = cells[i, j];
                if (cell.IsEditable)
                {
                    cell.SetValue(solutionBoard[i, j], notify: false);
                }
            }
        }
        
        HandleCorrectInput();
    }
    
    #region Helpers for save system

    public int[] CompressPuzzleTemplate()
    {
        return ArrayFunctions.Flatten2DArray(puzzleTemplate);
    }

    public int[] CompressSolutionBoard()
    {
        return ArrayFunctions.Flatten2DArray(solutionBoard);
    }

    public int[] CompressCurrentBoard()
    {
        return ArrayFunctions.Flatten2DArray(CurrentBoard);
    }
    
    #endregion
    
}
