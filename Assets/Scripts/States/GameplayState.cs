using System;
using UnityEngine;

namespace States
{
    public class GameplayState : IGameState
    {
        private UIManager uiManager;
        public float Timer { get; private set; } = 0f;
        private SaveData saveData;
        public GameplayState(UIManager uiManager)
        {
            this.uiManager = uiManager;
        }
        public void Enter()
        {
            if (StateManager.Instance.LoadPrevious)
            {
                saveData = StateManager.Instance.CachedData;
                LoadSavedGame();
            }
            else
            {
                StartNewGame();
            }
            uiManager.ShowGameScreen();
        }

        public void Exit()
        {
            BoardManager.Instance.ClearBoard();
        }

        public void Update()
        {
            Timer += Time.deltaTime; 
            uiManager.UpdateTimerDisplay(Timer);
        }

        private void LoadSavedGame()
        {
            int[,] puzzleTemplate = ArrayFunctions.UnflattenArray(saveData.puzzleBoard);
            int[,] solvedBoard = ArrayFunctions.UnflattenArray(saveData.solvedBoard);
            int[,] currentBoard = ArrayFunctions.UnflattenArray(saveData.currentBoard);
            
            BoardManager.Instance.LoadSavedGrids(puzzleTemplate, solvedBoard, currentBoard);
            BoardManager.Instance.SetBoardFromLoad();
            Timer = saveData.timer;
            uiManager.UpdateTimerDisplay(Timer);
            uiManager.UpdateMistakeText(saveData.mistakes);
        }

        private void StartNewGame()
        {
            BoardManager.Instance.SetNewBoard();
            Timer = 0f;
        }
    }
}
