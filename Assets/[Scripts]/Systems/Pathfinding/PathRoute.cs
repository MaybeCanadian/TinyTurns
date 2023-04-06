using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathRoute
{
    public List<GridNode> path = null;

    #region Set Up
    public PathRoute()
    {
        path = new List<GridNode>();
    }
    public void AddPointToPath(GridNode node)
    {
        path.Add(node);
    }
    #endregion

    #region Path Points
    public GridNode GetNodeinPath(int index)
    {
        if(index < 0 || index > path.Count - 1)
        {
            Debug.LogError("ERROR - Could not get grid node at given index, index out of range.");
            return null;
        }

        return path[index];
    }
    public bool CheckAtEnd(int index)
    {
        if(index == path.Count)
        {
            return true;
        }

        return false;
    }
    #endregion

    #region Debug
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
    #endregion
}
