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
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            
        }
    }
}
