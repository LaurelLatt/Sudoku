using UnityEngine;

namespace States
{
    public class ResultsState : IGameState
    {
        private UIManager uiManager;
        public ResultsState(UIManager uiManager)
        {
            this.uiManager = uiManager;
        }
        public void Enter()
        {
            uiManager.ShowResultsScreen();
            uiManager.UpdateBestTimeDisplay(SettingsManager.BestTime);
            uiManager.UpdateGamesPlayedText(SettingsManager.GamesPlayed);
            uiManager.UpdateWinRateText(SettingsManager.GamesPlayed, SettingsManager.GamesCompleted);
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            
        }
    }
}
