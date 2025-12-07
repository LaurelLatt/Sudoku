using UnityEngine;

namespace States
{
    public class PauseState : IGameState
    {
        private UIManager uiManager;
        private GameplayState gameplayState;
        public PauseState(UIManager uiManager, GameplayState gameplayState)
        {
            this.uiManager = uiManager;
            this.gameplayState = gameplayState;
        }
        public void Enter()
        {
            uiManager.ShowPauseScreen();
            Time.timeScale = 0f;
        }

        public void Exit()
        {
            SaveGame();
            
            gameplayState.ResumeFromPause();
            
            Time.timeScale = 1f;
            uiManager.HidePauseScreen();
        }

        public void Update() { }
        
        private void SaveGame()
        {
            SaveData data = new SaveData
            {
                currentBoard = ArrayFunctions.CopyJagged(BoardManager.Instance.CurrentBoard),
                puzzleBoard = ArrayFunctions.CopyJagged(BoardManager.Instance.puzzleTemplate),
                solvedBoard = ArrayFunctions.CopyJagged(BoardManager.Instance.solutionBoard),
                timer = gameplayState.Timer,
                mistakes = BoardManager.Instance.MistakeCount,
                mistakesEnabled = BoardManager.Instance.MistakesOn
            };

            SaveSystem.Save(data);
        }
    }
}
