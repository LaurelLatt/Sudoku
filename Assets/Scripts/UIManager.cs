using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject resultsPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Text mistakesText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text gamesPlayedText;
    [SerializeField] private Text winRateText;
    [SerializeField] private Text bestTimeText;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Toggle mistakesToggle;
    [SerializeField] private Toggle timerToggle;
    private GameObject currentPanel;
    
    // Show start screen at start of game
    void Start()
    {
        BoardManager.Instance.OnMistakeCountChanged += UpdateMistakeText;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    #region ScreenSetup
    
    // shows start screen and hides all other screens
    public void ShowStartScreen()
    {
        HideAllScreens();
        startPanel.SetActive(true);
    }

    // shows game screen and hides all other screens
    public void ShowGameScreen()
    {
        HideAllScreens();
        gamePanel.SetActive(true);
    }

    // shows results screen and hides all other screens
    public void ShowResultsScreen()
    {
        HideAllScreens();
        resultsPanel.SetActive(true);
    }

    public void ShowPauseScreen()
    {
        pausePanel.SetActive(true);
    }

    public void HidePauseScreen()
    {
        pausePanel.SetActive(false);
    }
    private void HideAllScreens()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(false);
        resultsPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
    
    #endregion
    
    // closes the entire application
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // works in editor only
#endif
    }

    // restarts the application to the start screen
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetWinText(bool gameWon)
    {
        gameOverText.text = gameWon ? "You Win!" : "Game Over";
    }
    
    public void UpdateGamesPlayedText(int count)
    {
        gamesPlayedText.text = count.ToString();
    }

    public void UpdateWinRateText(int gamesPlayed, int gamesCompleted)
    {
        
        float percentage = ((float)gamesCompleted / gamesPlayed) * 100f;
        
        winRateText.text = Math.Floor(percentage) + "%";
    }
    
    #region Mistakes
        
        public void UpdateMistakeText(int count)
        {
            mistakesText.text = "Mistakes: " + count.ToString();
        }
        
        private void OnMistakesToggleChanged(bool value)
        {
            SettingsManager.SetMistakesEnabled(value);
            mistakesText.gameObject.SetActive(value);
            if (!value)
            {
                mistakesToggle.interactable = false;
            }
        }
        
        public void SetUpMistakeUI()
        {
            mistakesToggle.isOn = SettingsManager.MistakesEnabled;
            mistakesText.gameObject.SetActive(SettingsManager.MistakesEnabled);
            mistakesToggle.onValueChanged.AddListener(OnMistakesToggleChanged);
            mistakesToggle.onValueChanged.AddListener(BoardManager.Instance.ToggleMistakesOn);
        }
    
        #endregion
        
    #region Timer functions
    public void UpdateTimerDisplay(float time)
    {
        timerText.text = TimeToString(time);
    }

    private void OnTimerToggleChanged(bool value)
    {
        SettingsManager.SetTimerEnabled(value);
        timerText.gameObject.SetActive(value);
        
    }
    
    public void SetUpTimerUI()
    {
        timerToggle.isOn = SettingsManager.TimerEnabled;
        timerText.gameObject.SetActive(SettingsManager.TimerEnabled);
        timerToggle.onValueChanged.AddListener(OnTimerToggleChanged);
    }

    public void UpdateBestTimeDisplay(float time)
    {
        if (time < 0)
        {
            bestTimeText.text = "N/A";
        }
        bestTimeText.text = TimeToString(time);
    }

    private string TimeToString(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        string text = $"{minutes:0}:{seconds:00}";
        return text;
    }
    
    #endregion

    public void ClearSettings()
    {
        Debug.Log("Cleared UI");
        SettingsManager.ClearAllSettings();
    }
}
