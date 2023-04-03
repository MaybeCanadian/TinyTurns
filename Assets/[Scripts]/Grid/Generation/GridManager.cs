using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridManager
{
    public static Grid grid = null;

    #region Init Functions
    public static void OutSideInit()
    {
        CheckInit();
    }
    private static void CheckInit()
    {
        Init();
    }
    private static void Init()
    {

    }
    #endregion

    #region Grid Functions
    /// <summary>
    /// Creates a grid with the specifications given. This grid can be gotten through GetGrid Directly.
    /// </summary>
    /// <param name="gridX"></param>
    /// <param name="gridY"></param>
    /// <param name="XLength"></param>
    /// <param name="YLength"></param>
    public static void CreateGrid(int gridX, int gridY, float XLength, float YLength)
    {
        grid = new Grid(gridX, gridY, XLength, YLength);
    }
    /// <summary>
    /// Returns the grid that is currently made. If no grid is made this will return null.
    /// </summary>
    /// <returns></returns>
    public static Grid GetGrid()
    {
        return grid;
    }
    #endregion
}
