using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[System.Serializable]
public class Grid
{
    public Tilemap tileMap = null;
    public GameObject gridParent = null;

    public GridNode[,] nodes = null;
    private int gridLength;
    private int gridHeight;
    private MapBounds bounds;

    #region Init Functions
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

        gridLength = bounds.xMax - bounds.xMin;
        gridHeight = bounds.yMax - bounds.yMin;

        nodes = new GridNode[gridLength, gridHeight];

        int ittX = 0;
        int ittY = 0;

        for(int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for(int y = bounds.yMin; y < bounds.yMax; y++) 
            {
                Vector3 worldPos = new Vector3(x, y, 0.0f) + tileCenterOffset;
                Vector2Int gridPos = new Vector2Int(ittX, ittY);

                if (tileMap.HasTile(new Vector3Int(x, y, 0)))
                {
                    GridNode node = CreateNode(worldPos, gridPos);
                    ConnectNodeNeighbours(node, ittX, ittY);
                    nodes[ittX, ittY] = node;

                    CreateNodeOBJ(worldPos, new Vector2Int(ittX, ittY));

                    node.SetWalkable(true);
                }
                else
                {
                    nodes[ittX, ittY] = null;
                    //CreateNodeOBJ(worldPos, new Vector2Int(ittX, ittY));
                }

                ittY++;
            }

            ittY = 0;
            ittX++;
        }

        foreach(GridNode node in nodes)
        {
            if(node != null)
            {
                node.DetermineCorners();
            }
        }
    }
    #endregion

    #region Node Control
    private GridNode CreateNode(Vector3 worldPos, Vector2Int gridPos)
    {
        GridNode node = new GridNode(worldPos, gridPos);

        return node;
    }
    private void CreateNodeOBJ(Vector3 worldPos, Vector2Int gridPos)
    {
        GameObject nodeOBJ = new GameObject();
        nodeOBJ.transform.SetParent(gridParent.transform);
        nodeOBJ.transform.position = worldPos;
        nodeOBJ.name = "node: " + gridPos.x + ", " + gridPos.y;
    }
    public GridNode GetNode(int x, int y)
    {
        if(x < 0 || y < 0 || x > gridLength - 1 || y > gridHeight - 1)
        {
            Debug.LogError("ERROR - Given gridPos is outside of the bounds of the grid.");
            return null;
        }

        return nodes[x, y];
    }
    private void ConnectNodeNeighbours(GridNode node, int x, int y)
    {
        if(node == null)
        {
            Debug.LogError("ERROR - Could not connect neighbours as node is null.");
            return;
        }

        if(x > 0)
        {
            GridNode leftNode = GetNode(x - 1, y);
            node.SetNeighbour(NodeDirections.LEFT, leftNode);

            if (leftNode != null)
            {
                leftNode.SetNeighbour(NodeDirections.RIGHT, node);
            }
        }

        if(y > 0)
        {
            GridNode downNode = GetNode(x, y - 1);
            node.SetNeighbour(NodeDirections.DOWN, downNode);
            if (downNode != null)
            {
                downNode.SetNeighbour(NodeDirections.UP, node);
            }
        }

        return;
    }
    #endregion

    #region Data
    public bool FindGridPosFromWorldPos(Vector3 worldPos, out Vector2Int gridPos)
    {
        gridPos = Vector2Int.zero;

        float x = (worldPos.x - bounds.xMin) / bounds.tileSize.x;
        float y = (worldPos.y - bounds.yMin) / bounds.tileSize.y;

        gridPos.x = (int)x;
        gridPos.y = (int)y;

        if(gridPos.x < 0 || gridPos.x > gridLength - 1 || gridPos.y < 0 || gridPos.y > gridHeight - 1)
        {
            return false;
        }

        if (nodes[gridPos.x, gridPos.y] == null)
        {
            return false;
        }

        return true;
    }
    public Vector2Int GetRandomWalkableLocationOnGrid(int maxTries = 10)
    {
        int tryCount = 0;

        while (true)
        {
            int x = Random.Range(0, gridLength);
            int y = Random.Range(0, gridHeight);

            if (nodes[x, y] != null)
            {
                return new Vector2Int(x, y);
            }

            tryCount++;

            if (tryCount > maxTries)
            {
                return Vector2Int.zero;
            }
        }
    }
    #endregion
}

[System.Serializable]
public struct MapBounds
{
    public int xMin, xMax;
    public int yMin, yMax;

    public Vector3 tileSize;
}
