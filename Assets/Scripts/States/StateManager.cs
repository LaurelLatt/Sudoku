using UnityEngine;

namespace States
{
    public class StateManager : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;
        
        private IGameState currentState;
        private IGameState menuState;
        private IGameState gameplayState;
        private IGameState pauseState;
        private IGameState resultsState;

        private void OnEnable()
        {
            Application.quitting += OnGameQuit;
        }

        private void OnDisable()
        {
            Application.quitting -= OnGameQuit;
        }
        
        private void Awake()
        {
            menuState = new MenuState(uiManager);
            gameplayState = new GameplayState(uiManager);
            pauseState = new PauseState(uiManager);
            resultsState = new ResultsState(uiManager);
            
            // set menu as start
            ChangeState(menuState);
        }
        private void ChangeState(IGameState newState) {
            
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }
        
        private void OnGameQuit()
        {
            currentState?.Exit();
        }

        public void ChangeToMenuState() => ChangeState(menuState);
        public void ChangeToGameplayState() => ChangeState(gameplayState);
        public void ChangeToPauseState() => ChangeState(pauseState);
        public void ChangeToResultsState() => ChangeState(resultsState);
    }
}
