using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int[][] currentBoard;
    public int[][] solvedBoard;
    public int[][] puzzleBoard;

    public float timer;
    public int mistakes;
}
public static class SaveSystem
{
    private static string SavePath =>
        Path.Combine(Application.persistentDataPath, "savegame.json");

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log(json);
    }

    public static SaveData Load()
    {
        if (!File.Exists(SavePath))
            return null;

        string json = File.ReadAllText(SavePath);
        return JsonUtility.FromJson<SaveData>(json);
    }
    
}
