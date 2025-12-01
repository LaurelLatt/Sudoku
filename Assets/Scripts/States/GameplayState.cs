using UnityEngine;

namespace States
{
    public class GameplayState : IGameState
    {
        private UIManager uiManager;
        public GameplayState(UIManager uiManager)
        {
            this.uiManager = uiManager;
        }
        public void Enter()
        {
            uiManager.ShowGameScreen();
            BoardManager.Instance.SetBoard();
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            
        }
    }
}
