using UnityEngine;

public static class SettingsManager
{
    private const string KeyTimer = "Setting_TimerEnabled";
    private const string KeyMistakes = "Setting_MistakesEnabled";
    private const string KeyGamesPlayed = "Stats_GamesPlayed";
    private const string KeyGamesCompleted = "Stats_GamesCompleted";
    private const string KeyBestTime = "Stats_BestTime";

    public static int GamesPlayed => PlayerPrefs.GetInt(KeyGamesPlayed, 0);
    public static int GamesCompleted => PlayerPrefs.GetInt(KeyGamesCompleted, 0);
    public static float BestTime => PlayerPrefs.GetFloat(KeyBestTime, -1f);
    public static bool MistakesEnabled => PlayerPrefs.GetInt(KeyMistakes, 1) == 1;
    public static bool TimerEnabled => PlayerPrefs.GetInt(KeyTimer, 1) == 1;

    public static void SetTimerEnabled(bool value)
    {
        PlayerPrefs.SetInt(KeyTimer, value ? 1 : 0);
    }

    public static void SetMistakesEnabled(bool value)
    {
        PlayerPrefs.SetInt(KeyMistakes, value ? 1 : 0);
    }

    public static void AddGamePlayed()
    {
        PlayerPrefs.SetInt(KeyGamesPlayed, GamesPlayed + 1);
    }

    public static void AddGameCompleted(float completionTime)
    {
        Debug.Log($"Adding game completed: {GamesCompleted + 1}");
        PlayerPrefs.SetInt(KeyGamesCompleted, GamesCompleted + 1);

        if (BestTime < 0 || completionTime < BestTime)
            PlayerPrefs.SetFloat(KeyBestTime, completionTime);
    }
}
