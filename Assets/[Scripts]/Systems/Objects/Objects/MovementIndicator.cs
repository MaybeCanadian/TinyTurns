using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementIndicator : UIObject
{
    bool showTrail = false;
    bool onNodes;
    UIList trailUI;
    GridNode startNode = null;

    #region Init Functions
    public MovementIndicator() : base(UIList.MovementIndicator)
    {

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
    protected override void HandleFollowCursorMove()
    {
        base.HandleFollowCursorMove();

        if(currentGridNode != null)
        {

        }
    }
    private void CreateTrail()
    {

    }

    #endregion
}
