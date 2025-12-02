using System.Collections.Generic;
using UnityEngine;

public static class ArrayFunctions
{
    public static int[] Flatten2DArray(int[,] board)
    {
        int[] flat = new int[81];
        int index = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                flat[index++] = board[i, j];
            }
        }
        return flat;
    }
    
    public static int[,] UnflattenArray(int[] flat)
    {
        int[,] board = new int[9, 9];
        int index = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                board[i, j] = flat[index++];
            }
        }
        return board;
    }
    
    public static int[,] Copy2DArray(int[,] source)
    {
        int rows = source.GetLength(0);
        int cols = source.GetLength(1);

        int[,] result = new int[rows, cols];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                result[r, c] = source[r, c];
            }
        }

        return result;
    }
}
