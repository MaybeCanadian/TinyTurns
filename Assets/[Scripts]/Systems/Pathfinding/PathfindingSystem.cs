using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class PathfindingSystem
{
    public static bool FindPathBetweenNodes(GridNode startNode, GridNode endNode, bool solid, Factions faction, out PathRoute route)
    {
        route = null;

        if (startNode == null)
        {
            Debug.LogError("ERROR - Cannot Pathfind, start is null.");
            return false;
        }

        if(endNode == null)
        {
            Debug.LogError("ERROR - Cannot Pathfind, end is null.");
            return false;
        }

        if(startNode == endNode)
        {
            Debug.Log("Already at location.");
            return false;
        }

        PathOperation pathingOperation = new (startNode, endNode, solid, faction);

        if (!pathingOperation.StartOperation())
        {
            return false;
        }

        route = pathingOperation.GetRoute();
        return true;
    }
}
