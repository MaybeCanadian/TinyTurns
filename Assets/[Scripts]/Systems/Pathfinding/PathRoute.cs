using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathRoute
{
    public List<GridNode> path = null;

    public PathRoute()
    {
        path = new List<GridNode>();
    }
    public void AddPointToPath(GridNode node)
    {
        path.Add(node);
    }
    public void DebugPrintSize()
    {
        Debug.Log("Path size is " + path.Count);
    }
    public void DebugPrintPath()
    {
        string pathString = "Path: ";
        foreach(GridNode node in path)
        {
            pathString += " - " + node.GetGridPos().x + "," + node.GetGridPos().y;
        }

        Debug.Log(pathString);
    }
}
