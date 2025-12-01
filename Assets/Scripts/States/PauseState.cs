using UnityEngine;

namespace States
{
    public class PauseState : IGameState
    {
        private UIManager uiManager;
        public PauseState(UIManager uiManager)
        {
            this.uiManager = uiManager;
        }
        public void Enter()
        {
            uiManager.ShowPauseScreen();
            Time.timeScale = 0f;
        }

        public void Exit()
        {
            Time.timeScale = 1f;
            uiManager.HidePauseScreen();
        }

        public void Update()
        {
            
        }
    }
}
