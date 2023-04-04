using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class GridManager
{
    private static Grid mapGrid = null;
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
}
