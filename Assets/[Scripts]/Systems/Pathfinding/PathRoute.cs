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
}
