using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedNodeIndicator : PathfindingObject
{
    public SelectedNodeIndicator(PathfindingObjectData obj) : base(obj)
    {
        entityID = EntityList.Movement;
        objectName = "movement indicator";
    }
    protected override void SetUpObject()
    {
        objType = ObjectTypeFilters.UI;
    }
    protected override void ConnectEvents()
    {
        base.ConnectEvents();
    }
    protected override void DisconnectEvents()
    {
        base.DisconnectEvents();
    }
    public void FollowCursor()
    {
        GridNode.OnNodeEnter += OnNodeEnter;
    }
    public void StopFollowingCursor()
    {
        GridNode.OnNodeEnter -= OnNodeEnter;
    }

    private void OnNodeEnter(GridNode node) 
    {
        PlaceObjectAtGridPos(node);
    }
}
