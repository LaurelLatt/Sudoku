using UnityEngine;

namespace States
{
    public class StateManager : MonoBehaviour
    {
        private IGameState currentState;
        private IGameState menuState;
        private IGameState gameplayState;
        private IGameState pauseState;
        private IGameState resultsState;

        private void Awake()
        {
            menuState = new MenuState();
            gameplayState = new GameplayState();
            pauseState = new PauseState();
            resultsState = new ResultsState();
            
            // set menu as start
            ChangeState(menuState);
        }
        public void ChangeState(IGameState newState) {
            
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }
    }
}
