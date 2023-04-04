using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridNode
{
    public Vector3 worldPos;
    public Vector3Int gridPos;

    public GridNode(Vector3 worldPos, Vector3Int gridPos)
    {
        this.worldPos = worldPos;
        this.gridPos = gridPos;
    }
}
