using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid : MonoBehaviour
{
    public Tilemap tileMap = null;

    private MapBounds bounds;

    public Grid(Tilemap walkAbleTileMap)
    {
        tileMap = walkAbleTileMap;

        GenerateMapBounds();

        GenerateGrid();
    }
    private void GenerateMapBounds()
    {
        bounds = new MapBounds();

        bounds.xMin = tileMap.cellBounds.xMin;
        bounds.xMax = tileMap.cellBounds.xMax;
        bounds.yMin = tileMap.cellBounds.yMin;
        bounds.yMax = tileMap.cellBounds.yMax;

        bounds.xGap = tileMap.cellGap.x;
        bounds.yGap = tileMap.cellGap.y;
    }
    private void GenerateGrid()
    {
        for(int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for(int y = bounds.yMin; y < bounds.yMax; y++) 
            {
                
            }
        }
    }
}

[System.Serializable]
public struct MapBounds
{
    public int xMin, xMax;
    public int yMin, yMax;

    public float xGap;
    public float yGap;
}
