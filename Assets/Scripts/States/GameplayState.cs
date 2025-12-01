using UnityEngine;

namespace States
{
    public class GameplayState : IGameState
    {
        private UIManager uiManager;
        private float timer = 0f;
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
            BoardManager.Instance.ClearBoard();
        }

        public void Update()
        {
            timer += Time.deltaTime; 
            uiManager.UpdateTimerDisplay(timer);
        }
    }
}
