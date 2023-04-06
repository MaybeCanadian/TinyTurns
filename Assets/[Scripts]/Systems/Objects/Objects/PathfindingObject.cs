using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingObject : Object
{
    protected PathRoute currentRoute = null;
    protected GridNode currentTargetNode = null;
    protected int currentRouteIndex = -1;

    private float lerpTimer = 0.0f;

    public PathfindingObject(PathfindingObjectData data) : base(data)
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

        //if(currentRoute != null)
        //{
        //    FollowCurrentRoute(fixedDelta);
        //}
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

        Debug.Log("Attempting to path from " + currentGridNode.GetGridPos() + " to " + targetNode.GetGridPos());

        if (!PathfindingSystem.FindPathBetweenNodes(currentGridNode, targetNode, out PathRoute route))
        {
            Debug.Log("Could not find a path to the given grid node.");
            return;
        }

        Debug.Log("Found a path");

        //currentRoute = route;
        //currentRouteIndex = -1;
        ////GetNextRoutePoint();
        //lerpTimer = 0.0f;
        //currentTargetNode = null;
    }
    protected void FollowCurrentRoute(float fixedDelta)
    {
        lerpTimer += (data as PathfindingObjectData).moveSpeed * fixedDelta;

        if(currentTargetNode != null)
        {
            worldPos = Vector3.Lerp(currentGridNode.GetWorldPos(), currentTargetNode.GetWorldPos(), lerpTimer);
        }

        if(lerpTimer >= 1.0f)
        {
            PlaceObjectAtGridPos(new Vector2Int(currentTargetNode.GetGridPos().x, currentTargetNode.GetGridPos().y));

            GetNextRoutePoint();

            lerpTimer = 0.0f;
        }
    }
    protected void GetNextRoutePoint()
    {
        currentRouteIndex++;

        if (currentRoute == null)
        {
            Debug.LogError("ERROR - Could not get next route point as the current route is null.");
            return;
        }

        if (currentRoute.CheckAtEnd(currentRouteIndex))
        {
            currentRoute = null;
            Debug.Log("Arrived");
            return;
        }

        currentTargetNode = currentRoute.GetNodeinPath(currentRouteIndex);

        if(currentTargetNode == null)
        {
            currentRoute = null;
            Debug.Log("Something went wrong with the route");
            return;
        }

        return;
    }
    #endregion
}
