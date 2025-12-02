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
            Time.timeScale = 1f;
            uiManager.HidePauseScreen();
        }

        public void Update() { }
        
        private void SaveGame()
        {
            SaveData data = new SaveData
            {
                currentBoard = BoardManager.Instance.CompressCurrentBoard(),
                puzzleBoard = BoardManager.Instance.CompressPuzzleTemplate(),
                solvedBoard = BoardManager.Instance.CompressSolutionBoard(),
                mistakes = BoardManager.Instance.MistakeCount,
                timer = gameplayState.Timer
            };

            SaveSystem.Save(data);
        }
    }
}
