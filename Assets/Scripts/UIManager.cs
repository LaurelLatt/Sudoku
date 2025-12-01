using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject resultsPanel;
    [SerializeField] private GameObject pausePanel;
    private GameObject currentPanel;
    
    // Show start screen at start of game
    void Start()
    {
        ShowStartScreen();
    }
    
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

    

    private void HideAllScreens()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(false);
        resultsPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
}
