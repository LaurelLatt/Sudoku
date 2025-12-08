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
                BoardManager.OnMistakesLimitReached += HandleMistakesLimitReached;
                SetUpGame();
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
            BoardManager.OnMistakesLimitReached -= HandleMistakesLimitReached;
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

        private void SetUpGame()
        {
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
        private void LoadSavedGame()
        {
            BoardManager.Instance.LoadSavedGrids(saveData.puzzleBoard,
                saveData.solvedBoard, saveData.currentBoard);
            BoardManager.Instance.SetBoardFromLoad();
            Timer = saveData.timer;
            uiManager.UpdateTimerDisplay(Timer);
            uiManager.UpdateMistakeText(saveData.mistakes);
            BoardManager.Instance.MistakesOn = saveData.mistakesEnabled;
            uiManager.SetUpMistakeUI();
            uiManager.SetUpTimerUI();
        }

        private void StartNewGame()
        {
            BoardManager.Instance.SetNewBoard();
            Timer = 0f;
            BoardManager.Instance.ResetMistakes();
            uiManager.SetUpMistakeUI();
            uiManager.SetUpTimerUI();
        }
        
        private void HandlePuzzleComplete()
        {
            Debug.Log("HandlePuzzleComplete");
            if (SettingsManager.TimerEnabled)
            {
                SettingsManager.AddGameCompleted(Timer);
            }
            else
            {
                SettingsManager.AddGameCompleted(-1f);
            }
            uiManager.SetWinText(true);
            StateManager.Instance.ChangeToResultsState();
        }

        private void HandleMistakesLimitReached()
        {
            uiManager.SetWinText(false);
            StateManager.Instance.ChangeToResultsState();
        }
    }
}
