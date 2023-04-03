using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridNode
{
    Vector2Int gridPos;
    Vector3 worldPos;
    public GridNode(Vector2Int gridPos, Vector3 worldPos)
    {
        this.gridPos = gridPos;
        this.worldPos = worldPos;
    }
}
