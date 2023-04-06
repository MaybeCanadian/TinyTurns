using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class GridManager
{
    private static Grid mapGrid = null;

    static bool init = false;

    #region Init Functions
    public static void OutSideInit()
    {
        CheckInit();

        return;
    }
    private static void CheckInit()
    {
        if(init == false)
        {
            Init();
        }

        return;
    }
    private static void Init()
    {
        init = true;

        Debug.Log("Grid Manager Init.");
        return;
    }
    #endregion

    #region Map Generation
    public static Grid GenerateMapGrid()
    {
        Tilemap walkable = WalkableLayer.GetWalkableTileMap();

        if(walkable == null)
        {
            Debug.LogError("ERROR - Could not get the walkable tilemap layer.");
            return null;
        }

        mapGrid = new Grid(walkable);

        return mapGrid;
    }
    public static Grid GetMapGrid() 
    {
        return mapGrid;
    }
    #endregion

    #region Grid Functions
    public static bool GetWorldPosFromGridPos(Vector2Int gridPos, out Vector3 worldPos)
    {
        worldPos = Vector3.zero;

        if (mapGrid == null)
        {
            Debug.LogError("ERROR - Could not get a world positon as the grid is null.");
            return false;
        }

        GridNode node = mapGrid.GetNode(gridPos.x, gridPos.y);

        if(node == null)
        {
            Debug.LogError("ERROR - Could not a world position as the grid position is null.");
            return false;
        }

        worldPos = node.GetWorldPos();

        return true;
    }
    public static bool GetGridPosFromWorldPos(Vector3 worldPos, out Vector2Int gridPos)
    {
        gridPos = Vector2Int.zero;

        if (mapGrid == null)
        {
            Debug.LogError("ERROR - Could not get gridPos from world pos as the grid is null.");
            return false;
        }

        return mapGrid.FindGridPosFromWorldPos(worldPos, out gridPos);
    }
    public static GridNode GetGridNode(int x, int y)
    {
        if(mapGrid == null)
        {
            Debug.LogError("ERROR - Could not get grid node as grid is null.");
            return null;
        }

        return mapGrid.GetNode(x, y);
    }
    #endregion
}
