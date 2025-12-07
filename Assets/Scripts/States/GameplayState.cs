using System;
using UnityEngine;

namespace States
{
    public class GameplayState : IGameState
    {
        private UIManager uiManager;
        public float Timer { get; private set; } = 0f;
        private SaveData saveData;
        private bool resumed = false;
        public GameplayState(UIManager uiManager)
        {
            this.uiManager = uiManager;
        }
        public void Enter()
        {
            if (!resumed)
            {
                BoardManager.OnPuzzleCompleted += HandlePuzzleComplete;
                if (StateManager.Instance.LoadPrevious)
                {
                    saveData = StateManager.Instance.CachedData;
                    LoadSavedGame();
                }
                else
                {
                    StartNewGame();
                    SettingsManager.AddGamePlayed();
                }
            }
            else
            {
                resumed = false; // Clear for next time
            }
            
            uiManager.ShowGameScreen();
        }

        public void Exit()
        {
            BoardManager.Instance.ClearBoard();
            BoardManager.OnPuzzleCompleted -= HandlePuzzleComplete;
        }

        public void Update()
        {
            Timer += Time.deltaTime; 
            uiManager.UpdateTimerDisplay(Timer);
        }

        public void ResumeFromPause()
        {
            resumed = true;
        }
        private void LoadSavedGame()
        {
            BoardManager.Instance.LoadSavedGrids(saveData.puzzleBoard,
                saveData.solvedBoard, saveData.currentBoard);
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
        
        private void HandlePuzzleComplete()
        {
            SettingsManager.AddGameCompleted(Timer);
            StateManager.Instance.ChangeToResultsState();
        }
    }
}
