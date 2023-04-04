using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[System.Serializable]
public class Grid
{
    public Tilemap tileMap = null;
    public GameObject gridParent = null;

    public GridNode[,] nodes = null;

    private MapBounds bounds;

    public Grid(Tilemap walkAbleTileMap)
    {
        tileMap = walkAbleTileMap;

        GenerateMapBounds();

        GenerateGridParent();

        GenerateGrid();

        Debug.Log("Grid Set Up");

        return;
    }
    private void GenerateMapBounds()
    {
        bounds = new MapBounds();

        bounds.xMin = tileMap.cellBounds.xMin;
        bounds.xMax = tileMap.cellBounds.xMax;
        bounds.yMin = tileMap.cellBounds.yMin;
        bounds.yMax = tileMap.cellBounds.yMax;

        bounds.tileSize = tileMap.cellSize;
    }
    private void GenerateGridParent()
    {
        gridParent = new GameObject();
        gridParent.name = "[Grid]";
    }
    private void GenerateGrid()
    {
        Vector3 tileCenterOffset = new Vector3(bounds.tileSize.x / 2, bounds.tileSize.y / 2, 0.0f);

        nodes = new GridNode[bounds.xMax - bounds.xMin, bounds.yMax - bounds.yMax];

        int ittX = 0;
        int ittY = 0;

        for(int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for(int y = bounds.yMin; y < bounds.yMax; y++) 
            {
                Vector3 worldPos = new Vector3(x, y, 0.0f) + tileCenterOffset;
                Vector3Int gridPos = new Vector3Int(x, y, 0);

                GridNode node = CreateNode(worldPos, gridPos);

                nodes[ittX, ittY] = node;

                if (tileMap.HasTile(gridPos))
                {
                    
                }

                ittY++;
            }

            ittX++;
        }
    }
    private GridNode CreateNode(Vector3 worldPos, Vector3Int gridPos)
    {
        GridNode node = new GridNode(worldPos, gridPos);

        //debug make node obj to see in editor
        GameObject nodeOBJ = new GameObject();
        nodeOBJ.transform.SetParent(gridParent.transform);
        nodeOBJ.transform.position = worldPos;
        nodeOBJ.name = "node";


        return node;
    }
}

[System.Serializable]
public struct MapBounds
{
    public int xMin, xMax;
    public int yMin, yMax;

    public Vector3 tileSize;
}
