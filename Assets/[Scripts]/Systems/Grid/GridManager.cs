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
    public static Vector3 GetWorldPosFromGridPos(Vector2Int gridPos)
    {
        if (mapGrid == null)
        {
            return Vector3.zero;
        }

        GridNode node = mapGrid.GetNode(gridPos.x, gridPos.y);

        if(node == null)
        {
            return Vector3.zero;
        }

        return node.GetWorldPos();
    }
    #endregion
}
