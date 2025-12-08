using UnityEngine;

namespace States
{
    public class StateManager : MonoBehaviour
    {
        public static StateManager Instance { get; private set; }
        [SerializeField] private UIManager uiManager;
        
        private IGameState currentState;
        private MenuState menuState;
        private GameplayState gameplayState;
        private PauseState pauseState;
        private ResultsState resultsState;
        
        public bool LoadPrevious {get; private set;}
        public SaveData CachedData {get; private set;}
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        private void OnEnable()
        {
            Application.quitting += OnGameQuit;
        }

        private void OnDisable()
        {
            Application.quitting -= OnGameQuit;
        }
        
        private void Start()
        {
            menuState = new MenuState(uiManager);
            gameplayState = new GameplayState(uiManager);
            pauseState = new PauseState(uiManager, gameplayState);
            resultsState = new ResultsState(uiManager);
            
            ChangeState(menuState);
        }

        private void Update()
        {
            currentState?.Update();
        }
        
        private void ChangeState(IGameState newState, bool callExit = true)
        {
            if (currentState != null && callExit)
                currentState.Exit();

            currentState = newState;
            currentState.Enter();
        }
        
        private void OnGameQuit()
        {
            currentState?.Exit();
        }

        public void ChangeToMenuState() => ChangeState(menuState);
        public void ChangeToGameplayState() => ChangeState(gameplayState);
        public void ChangeToPauseState() => ChangeState(pauseState, callExit: false);
        public void ChangeToResultsState() => ChangeState(resultsState);
        
        public void StartNewGame()
        {
            LoadPrevious = false;
            ChangeToGameplayState();
        }

        public void ResumeGame()
        {
            LoadPrevious = true;
            CachedData = GameSessionSave.Load();
            if (CachedData != null)
            {
                ChangeToGameplayState();
            }
        }
    }
}
