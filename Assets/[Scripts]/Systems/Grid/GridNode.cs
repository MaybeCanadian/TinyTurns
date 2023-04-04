using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridNode
{
    private Vector3 worldPos;
    private Vector3Int gridPos;
    bool walkable = true;
    private GridNode[] neighbours;

    #region Init Functions
    public GridNode(Vector3 worldPos, Vector3Int gridPos)
    {
        this.worldPos = worldPos;
        this.gridPos = gridPos;

        SetUpNeighbourList();
    }
    private void SetUpNeighbourList()
    {
        neighbours = new GridNode[4]
        {
            null,
            null,
            null,
            null
        };
    }
    #endregion

    #region Neighbours
    public void SetNeighbour(NodeDirections direction, GridNode node)
    {
        neighbours[(int)direction] = node;
    }
    public List<GridNode> GetNeighbours()
    {
        List<GridNode> actualNeighbours = new List<GridNode>(); 

        foreach(GridNode neighbour in neighbours)
        {
            if (neighbour == null)
            {
                continue;
            }

            actualNeighbours.Add(neighbour);
        }

        return actualNeighbours;
    }
    public GridNode GetNeighbour(NodeDirections direction)
    {
        return neighbours[(int)direction];
    }
    #endregion

    #region Pathing
    public void SetWalkable(bool input)
    {
        walkable = input;
    }
    public Vector3 GetWorldPos()
    {
        return worldPos;
    }
    #endregion
}

[System.Serializable]
public enum NodeDirections
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}
