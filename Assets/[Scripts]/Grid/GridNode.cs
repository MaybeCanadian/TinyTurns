using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridNode
{
    public delegate void GridEventGeneric();
    public GridEventGeneric OnGridTileTypeChanged;

    public Vector2Int gridPos;
    public Vector3 worldPos;
    public GridTileTypes tileType = GridTileTypes.NULL;
    public GridNode(Vector2Int gridPos, Vector3 worldPos)
    {
        this.gridPos = gridPos;
        this.worldPos = worldPos;
    }

    public void SetGridTileType(GridTileTypes type)
    {
        tileType = type;

        OnGridTileTypeChanged?.Invoke();
    }
}
