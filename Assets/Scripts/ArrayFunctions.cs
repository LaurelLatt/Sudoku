using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ArrayFunctions
{
    public static T[][] CreateJagged<T>(int rows, int cols)
    {
        var arr = new T[rows][];
        for (int i = 0; i < rows; i++)
            arr[i] = new T[cols];
        return arr;
    }

    public static int[][] CreateJagged(int rows, int cols)
    {
        var arr = new int[rows][];
        for (int i = 0; i < rows; i++)
            arr[i] = new int[cols];
        return arr;
    }

    public static int[][] CopyJagged(int[][] source)
    {
        if (source == null) return null;

        int[][] copy = new int[source.Length][];
        for (int i = 0; i < source.Length; i++)
        {
            if (source[i] != null)
                copy[i] = source[i].ToArray();
            else
                copy[i] = new int[0]; 
        }
        return copy;
    }

    public static void PrintBoard( int[][] board)
    {
        if (board == null)
        {
            Debug.Log($"board is null!");
            return;
        }

        string output = "";
        for (int i = 0; i < board.Length; i++)
        {
            string rowStr = "";
            for (int j = 0; j < board[i].Length; j++)
            {
                rowStr += board[i][j] + " ";
            }
            output += rowStr + "\n";
        }

        Debug.Log(output);
    }
}
