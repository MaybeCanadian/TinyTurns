using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PathfindingObject : Object
{
    #region Event Dispatchers
    public delegate void PathfindingEvent();
    public PathfindingEvent OnPathFindingStarted;
    public PathfindingEvent OnPathFindingFinished;
    #endregion

    protected PathRoute currentRoute = null;
    protected GridNode currentTargetNode = null;
    protected int currentRouteIndex = -1;

    protected Animator anims = null;
    private float lerpTimer = 0.0f;

    protected float delay = 0.0f;
    protected float delayTimer = 0.0f;

    public PathfindingObject(PathfindingObjectData data) : base(data)
    {
        ConnectEvents();
    }
    protected override void SetUpObject()
    {
        objType = ObjectTypeFilters.Pathfinding;
    }

    #region Update Functions
    protected override void Update(float delta)
    {
        base.Update(delta);
    }
    protected override void FixedUpdate(float fixedDelta)
    {
        base.FixedUpdate(fixedDelta);

        if (currentRoute != null)
        {
            FollowCurrentRoute(fixedDelta);
        }
        else
        {
            //Wander(fixedDelta);
        }
    }
    protected override void LateUpdate(float delta)
    {
        base.LateUpdate(delta);
    }
    #endregion

    #region Pathing Functions
    public void PathToGridPosition(GridNode targetNode)
    {
        if (targetNode == null)
        {
            Debug.LogError("ERROR - Could not path to given position as that node is null.");
            return;
        }

        if (!PathfindingSystem.FindPathBetweenNodes(currentGridNode, targetNode, out PathRoute route))
        {
            Debug.Log("Could not find a path to the given grid node.");
            return;
        }

        currentRoute = route;
        currentRouteIndex = -1;
        GetNextRoutePoint();
        lerpTimer = 0.0f;

        if (anims != null)
        {
            anims.SetInteger("AnimState", (int)PathfindingAnimStates.WALKING);
        }
    }
    public void PathToGridPosition(Vector2Int targetPos)
    {
        GridNode targetNode = GridManager.GetGridNode(targetPos.x, targetPos.y);

        PathToGridPosition(targetNode);
    }
    protected void FollowCurrentRoute(float fixedDelta)
    {
        lerpTimer += (data as PathfindingObjectData).moveSpeed * fixedDelta;

        if(currentTargetNode != null)
        {
            worldPos = Vector3.Lerp(currentGridNode.GetWorldPos(), currentTargetNode.GetWorldPos(), lerpTimer);

            MoveObjToPosition();

            if (lerpTimer >= 1.0f)
            {
                PlaceObjectAtGridPos(new Vector2Int(currentTargetNode.GetGridPos().x, currentTargetNode.GetGridPos().y));

                GetNextRoutePoint();

                lerpTimer = 0.0f;
            }
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

            if(anims != null)
            {
                anims.SetInteger("AnimState", (int)PathfindingAnimStates.IDLE);
            }

            PathfindingFinished();
            return;
        }

        currentTargetNode = currentRoute.GetNodeinPath(currentRouteIndex);

        if(currentTargetNode == null)
        {
            currentRoute = null;
            Debug.Log("Something went wrong with the route");
            return;
        }

        direction = currentTargetNode.GetGridPos().x - currentGridNode.GetGridPos().x;

        SetOBJDirection();
    }
    protected void Wander(float fixedDelta)
    {
        delayTimer += fixedDelta;

        if (delayTimer >= delay)
        {
            delay = Random.Range(0.0f, 5.0f);

            delayTimer = 0.0f;

            Grid grid = GridManager.GetMapGrid();

            PathToGridPosition(grid.GetRandomWalkableLocationOnGrid());
        }
    }
    protected virtual void PathfindingFinished()
    {
        OnPathFindingFinished?.Invoke();
    }
    #endregion

    #region Visuals
    public override void CreateVisuals(bool remake = false)
    {
        base.CreateVisuals(remake);

        if(objectOBJ != null)
        {
            anims = objectOBJ.GetComponentInChildren<Animator>();
            //Debug.Log("found anims");
        }
    }
    public override void DestroyVisuals()
    {
        base.DestroyVisuals();

        anims = null;
    }
    #endregion
}

public enum PathfindingAnimStates
{
    IDLE,
    WALKING
}
