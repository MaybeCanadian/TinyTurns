using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathNode
{
    private float H;
    private float G;

    private GridNode node;
    private PathNode parentNode;

    private PathNodeEvalState evalState;

    #region Init Functions
    public PathNode(GridNode node)
    {
        this.node = node;
        parentNode = null;

        G = int.MaxValue;
        H = int.MaxValue;

        evalState = PathNodeEvalState.Open;
    }
    #endregion

    #region Costs
    public void SetHCost(float input)
    {
        H = input;
    }
    public float GetHCost()
    {
        return H;
    }
    public float GetGCost()
    {
        return G;
    }
    public float GetFCost()
    {
        return G + H;
    }
    #endregion

    #region Nodes
    public void SetParentNode(PathNode parent, float newG)
    {
        parentNode = parent;

        G = newG;
    }
    public GridNode GetBaseNode()
    {
        return node;
    }
    public PathNode GetParentNode()
    {
        return parentNode;  
    }
    public PathNodeEvalState GetEvalState()
    {
        return evalState;
    }
    public void SetEvalState(PathNodeEvalState newState)
    {
        evalState = newState;
    }
    #endregion
}

[System.Serializable]
public enum PathNodeEvalState
{
    Open,
    Closed
}
