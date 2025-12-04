using UnityEngine;

namespace States
{
    public class MenuState : IGameState
    {
        private UIManager uiManager;
        public MenuState(UIManager uiManager)
        {
            this.uiManager = uiManager;
        }
        public void Enter()
        {
            uiManager.ShowStartScreen();
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            
        }
    }
}
