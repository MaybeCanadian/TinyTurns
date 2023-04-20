using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridNode
{
    #region Event Dispatchers
    public delegate void GridNodeEvent(GridNode node);
    public static GridNodeEvent OnNodeEnter;
    public static GridNodeEvent OnNodeExit;
    public static GridNodeEvent OnNodeStay;

    public delegate void GridNodeMouseEvent(GridNode node, int button);
    public static GridNodeMouseEvent OnNodeMouseDown;
    public static GridNodeMouseEvent OnNodeMouseUp;
    public static GridNodeMouseEvent OnNodeMouseHold;
    #endregion

    private Vector3 worldPos;
    private Vector2Int gridPos;
    bool walkable = true;
    private GridNode[] neighbours;

    private List<Object> objectsOnNode = null;

    #region Init Functions
    public GridNode(Vector3 worldPos, Vector2Int gridPos)
    {
        this.worldPos = worldPos;
        this.gridPos = gridPos;

        objectsOnNode = new List<Object>();

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
    public bool GetIsPathable(Factions faction)
    {
        foreach(Object obj in objectsOnNode)
        {
            if(!obj.CanPathThrough(faction))
            {
                return false;
            }
        }

        return true;
    }
    public bool GetIsStandable(Factions faction)
    {
        foreach (Object obj in objectsOnNode)
        {
            if (!obj.CanStandOn(faction))
            {
                return false;
            }
        }

        return true;
    }
    #endregion

    #region Objects
    public void AddObjectToNode(Object obj)
    {
        objectsOnNode.Add(obj);
    }
    public void RemoveObjectFromNode(Object obj)
    {
        objectsOnNode.Remove(obj);
    }
    public bool IsObjectOnNode()
    {
        return objectsOnNode.Count > 0;
    }
    public List<Object> GetObjectsOnNode(ObjectTypeFilters filter = ObjectTypeFilters.None)
    {
        List<Object> objects = new List<Object>();

        foreach(Object obj in objectsOnNode)
        {
            if(filter == ObjectTypeFilters.None)
            {
                objects.Add(obj);
                continue;
            }

            if(obj.objType == filter)
            {
                objects.Add(obj);
                continue;
            }
        }

        return objects;
    }
    #endregion

    #region Input

    #region Mouse Inputs
    public void OnMouseDown(int button)
    {
        foreach(Object obj in objectsOnNode)
        {
            obj.OnMouseDown(button);
        }

        OnNodeMouseDown?.Invoke(this, button);
    }
    public void OnMouseUp(int button)
    {
        foreach(Object obj in objectsOnNode)
        {
            obj.OnMouseUp(button);
        }

        OnNodeMouseUp?.Invoke(this, button);
    }
    public void OnMouseHeld(int button)
    {
        foreach (Object obj in objectsOnNode)
        {
            obj.OnMouseHeld(button);
        }

        OnNodeMouseHold?.Invoke(this, button);
    }
    #endregion

    #region Mouse Movement
    public void OnMouseEnter()
    {
        foreach(Object obj in objectsOnNode)
        {
            obj.OnMouseEnter();
        }

        OnNodeEnter?.Invoke(this);
    }
    public void OnMouseExit()
    {
        foreach(Object obj in objectsOnNode)
        {
            obj.OnMouseExit();
        }

        OnNodeExit?.Invoke(this);
    }
    public void OnMouseStay()
    {
        foreach (Object obj in objectsOnNode)
        {
            obj.OnMouseStay();
        }

        OnNodeStay?.Invoke(this);
    }
    #endregion

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
