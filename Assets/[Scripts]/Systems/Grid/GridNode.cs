using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridNode
{
    private Vector3 worldPos;
    private Vector2Int gridPos;
    bool walkable = true;
    private GridNode[] neighbours;

    #region Init Functions
    public GridNode(Vector3 worldPos, Vector2Int gridPos)
    {
        this.worldPos = worldPos;
        this.gridPos = gridPos;

        SetUpNeighbourList();
    }
    private void SetUpNeighbourList()
    {
        neighbours = new GridNode[8]
        {
            null,
            null,
            null,
            null,
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
    public void DetermineCorners()
    {
        if (neighbours[(int)NodeDirections.LEFT] != null)
        {
            if (neighbours[(int)NodeDirections.DOWN] != null)
            {
                SetNeighbour(NodeDirections.DOWN_LEFT, neighbours[(int)NodeDirections.LEFT].GetNeighbour(NodeDirections.DOWN));
            }

            if (neighbours[(int)NodeDirections.UP] != null)
            {
                SetNeighbour(NodeDirections.UP_LEFT, neighbours[(int)NodeDirections.LEFT].GetNeighbour(NodeDirections.UP));
            }
        }

        if (neighbours[(int)NodeDirections.RIGHT] != null)
        {
            if (neighbours[(int)NodeDirections.DOWN] != null)
            {
                SetNeighbour(NodeDirections.DOWN_RIGHT, neighbours[(int)NodeDirections.RIGHT].GetNeighbour(NodeDirections.DOWN));
            }

            if (neighbours[(int)NodeDirections.UP] != null)
            {
                SetNeighbour(NodeDirections.UP_RIGHT, neighbours[(int)NodeDirections.RIGHT].GetNeighbour(NodeDirections.UP));
            }
        }
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
    public Vector2Int GetGridPos()
    {
        return gridPos;
    }
    #endregion
}

[System.Serializable]
public enum NodeDirections
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    UP_LEFT,
    UP_RIGHT,
    DOWN_LEFT,
    DOWN_RIGHT
}
