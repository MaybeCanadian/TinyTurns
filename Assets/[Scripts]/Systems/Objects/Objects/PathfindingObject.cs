using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingObject : Object
{
    protected PathRoute currentRoute = null;

    public PathfindingObject(ObjectData data) : base(data)
    {
        ConnectEvents();
    }

    #region Update Functions
    protected override void Update(float delta)
    {
        base.Update(delta);
    }
    protected override void FixedUpdate(float fixedDelta)
    {
        base.FixedUpdate(fixedDelta);
    }
    protected override void LateUpdate(float delta)
    {
        base.LateUpdate(delta);
    }
    #endregion

    #region Pathing Functions
    public void PathToGridPosition(Vector2Int targetPos)
    {
        GridNode targetNode = GridManager.GetGridNode(targetPos.x, targetPos.y);

        if (targetNode == null)
        {
            Debug.LogError("ERROR - Could not path to given position as that node is null.");
            return;
        }

        if(!PathfindingSystem.FindPathBetweenNodes(currentGridNode, targetNode, out PathRoute route))
        {
            Debug.Log("Could not find a path to the given grid node.");
            return;
        }

        currentRoute = route;
    }
    #endregion
}
