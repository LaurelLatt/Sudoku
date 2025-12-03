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
    private GameObject currentPanel;
    
    // Show start screen at start of game
    void Start()
    {
        ShowStartScreen();
        BoardManager.Instance.OnMistakeCountChanged += UpdateMistakeText;
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
    
    public void UpdateMistakeText(int count)
    {
        mistakesText.text = "Mistakes: " + count.ToString();
    }

    public void UpdateGamesPlayedText(int count)
    {
        gamesPlayedText.text = count.ToString();
    }

    public void UpdateWinRateText(int gamesPlayed, int gamesCompleted)
    {
        int percentage = gamesCompleted / gamesPlayed * 100;
        winRateText.text = percentage + "%";
    }
    public void UpdateTimerDisplay(float time)
    {
        timerText.text = TimeToString(time);
    }

    public void UpdateBestTimeDisplay(float time)
    {
        bestTimeText.text = TimeToString(time);
    }

    private string TimeToString(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        string text = $"{minutes:0}:{seconds:00}";
        return text;
    }
}
