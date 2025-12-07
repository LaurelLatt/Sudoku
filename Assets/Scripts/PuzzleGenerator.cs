using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class PuzzleSet
{
    public Puzzle[] puzzles;
}

[System.Serializable]
public class Puzzle
{
    public int id;
    public int[][] puzzleBoard;
    public int[][] solvedBoard;
}
public class PuzzleGenerator : MonoBehaviour
{
    public int[][] solvedPuzzle = new int[][]
    {
        new int[] {5,1,6,9,8,3,4,2,7},
        new int[] {7,9,3,2,1,4,6,8,5},
        new int[] {4,2,8,5,7,6,9,3,1},
        new int[] {1,5,7,3,6,2,8,4,9},
        new int[] {2,3,9,8,4,5,1,7,6},
        new int[] {8,6,4,7,9,1,3,5,2},
        new int[] {9,4,2,1,3,7,5,6,8},
        new int[] {3,7,1,6,5,8,2,9,4},
        new int[] {6,8,5,4,2,9,7,1,3}
    };
    public int[][] gamePuzzle = new int[][]
    {
        new int[] {5,1,6,0,0,3,4,2,0},
        new int[] {7,0,0,2,0,4,0,8,0},
        new int[] {0,2,8,5,7,0,0,0,1},
        new int[] {1,0,0,3,6,0,0,4,0},
        new int[] {2,3,0,0,0,0,0,7,6},
        new int[] {0,0,0,0,9,1,3,0,0},
        new int[] {0,4,0,0,0,7,5,6,0},
        new int[] {3,0,1,0,0,0,2,0,0},
        new int[] {0,8,0,0,0,9,0,0,3}
    };

    public int[][] GeneratePuzzle()
    {
        LoadPuzzleFromFile();
        return gamePuzzle;
    }

    private void LoadPuzzleFromFile()
    {
        TextAsset json = Resources.Load<TextAsset>("PuzzleSets/puzzles");
        if (json == null)
        {
            Debug.LogError("Puzzle JSON not found in Resources/PuzzleSets/");
        }

        PuzzleSet set =  JsonConvert.DeserializeObject<PuzzleSet>(json.text);
        Puzzle chosen = set.puzzles[Random.Range(0, set.puzzles.Length)];
        
        solvedPuzzle = ArrayFunctions.CopyJagged(chosen.solvedBoard);
        gamePuzzle = ArrayFunctions.CopyJagged(chosen.puzzleBoard);
    }
}
