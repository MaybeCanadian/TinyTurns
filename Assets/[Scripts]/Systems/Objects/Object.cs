using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object
{
    public Vector3 worldPos;
    public Vector2Int gridPos;

    public Object(Vector2Int startPos)
    {
        gridPos = startPos;
        worldPos = GridManager.GetWorldPosFromGridPos(gridPos);
    }
}
