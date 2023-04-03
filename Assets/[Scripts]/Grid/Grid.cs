using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Grid
{
    public GridNode[,] grid = null;

    public int gridX;
    public int gridY;
    public float XSize;
    public float YSize;

    public Grid(int x, int y, float XSize, float YSize)
    {
        this.gridX = x;
        this.gridY = y;
        this.XSize = XSize;
        this.YSize = YSize;

        BuildGrid();
    }
    private void BuildGrid()
    {
        grid = new GridNode[gridX,gridY];

        for(int x = 0; x < gridX; x++)
        {
            for(int y = 0; y < gridY; y++)
            {
                Vector2Int gridPos = new Vector2Int(x,y);
                Vector3 worldPos = new Vector3(x * XSize, y * YSize, 0.0f);

                grid[x, y] = new GridNode(gridPos, worldPos);
            }
        }
    }
}
