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
    public static void CreateGrid(int gridX, int gridY, float XLength, float YLength)
    {
        grid = new Grid(gridX, gridY, XLength, YLength);
    }
    public static Grid GetGrid()
    {
        return grid;
    }
    #endregion
}
