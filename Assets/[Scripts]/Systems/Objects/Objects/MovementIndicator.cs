using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementIndicator : UIObject
{
    bool showTrail = false;
    bool onNodes;
    UIList trailUI;
    GridNode startNode = null;

    List<UIObject> trailList = null;

    #region Init Functions
    public MovementIndicator() : base(UIList.MovementIndicator)
    {
        trailList = new List<UIObject>();
    }
    #endregion

    #region Pathfinding
    public void AddTrail(UIList trailUI, GridNode startNode, bool onNodes = true)
    {
        showTrail = true;
        this.startNode = startNode;
        this.onNodes = onNodes;
        this.trailUI = trailUI;
    }
    public void RemoveTrail()
    {
        showTrail = false;
        RemoveOldTrail();
    }
    protected override void HandleFollowCursorMove()
    {
        base.HandleFollowCursorMove();

        //Debug.Log("test");
        if (showTrail == true)
        {
            CreateTrail(startNode, currentGridNode);
        }

    }
    private void CreateTrail(GridNode start, GridNode end)
    {
        if (currentGridNode == null || startNode == null)
        {
            //RemoveOldTrail();
            return;
        }

        if(!PathfindingSystem.FindPathBetweenNodes(start, end, out PathRoute route)) 
        {
            RemoveOldTrail();
            return;
        }

        TryUtilizeOldPath(route);
    }
    private void CreateTrailUIAtGridNode(GridNode node)
    {
        UIObject uiOBJ = new UIObject(trailUI);

        uiOBJ.PlaceObjectAtGridPos(node);
        uiOBJ.CreateVisuals();

        trailList.Add(uiOBJ);
    }
    private void RemoveOldTrail()
    {
        foreach(UIObject obj in trailList)
        {
            obj.DestroyObject();
        }

        trailList.Clear();
    }
    private void TryUtilizeOldPath(PathRoute route)
    {
        int trailItt = 0;
        for(int i = 0; i < route.path.Count; i++)
        {
            if(i == 0 || i == route.path.Count - 1)
            {
                continue;
            }


            if(trailItt < trailList.Count)
            {
                trailList[trailItt].PlaceObjectAtGridPos(route.path[i]);
                trailList[trailItt].CreateVisuals();
            }
            else
            {
                CreateTrailUIAtGridNode(route.path[i]);
            }

            trailItt++;
        }

        if(trailItt < trailList.Count)
        {
            for(int i = trailItt; i < trailList.Count; i++)
            {
                trailList[i].DestroyObject();
            }

            int count = trailList.Count - trailItt - 1;
            trailList.RemoveRange(trailItt, count);
        }

        Debug.Log(trailList.Count);
    }

    #endregion
}
