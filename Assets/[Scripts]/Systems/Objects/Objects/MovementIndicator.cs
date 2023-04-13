using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementIndicator : Object
{
    public MovementIndicator() : base(null)
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
