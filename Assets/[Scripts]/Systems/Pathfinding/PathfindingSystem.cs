using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class PathfindingSystem
{
    public static bool FindPathBetweenNodes(GridNode startNode, GridNode endNode, out PathRoute route)
    {
        PathOperation pathingOperation = new (startNode, endNode);
        route = null;

        if(!pathingOperation.StartOperation())
        {
            return false;
        }

        route = pathingOperation.GetRoute();
        return true;
    }
}
