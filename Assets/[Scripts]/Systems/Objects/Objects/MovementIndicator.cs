using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementIndicator : UIObject
{
    //private bool followingMouseCursor = false;

    public MovementIndicator() : base(UIList.MovementIndicator)
    {

    }
    //protected override void SetUpObject()
    //{
    //    objType = ObjectTypeFilters.UI;
    //}
    //protected override void ConnectEvents()
    //{
    //    base.ConnectEvents();
    //}
    //protected override void DisconnectEvents()
    //{
    //    base.DisconnectEvents();
    //}
    //public void FollowCursor()
    //{
    //    if(followingMouseCursor == true)
    //    {
    //        return;
    //    }

    //    followingMouseCursor = true;
    //    GridNode.OnNodeEnter += OnNodeEnter;
    //}
    //public void StopFollowingCursor()
    //{
    //    if(followingMouseCursor == false)
    //    {
    //        return;
    //    }

    //    followingMouseCursor = false;
    //    GridNode.OnNodeEnter -= OnNodeEnter;
    //}

    //private void OnNodeEnter(GridNode node) 
    //{
    //    PlaceObjectAtGridPos(node);
    //}
}
