using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject : Object
{
    UIList ui;
    bool followingCursor = false;

    #region Init Functions
    public UIObject(UIList uiType) : base(null)
    {
        ui = uiType;
    }
    #endregion

    #region UI Functions
    public void FollowCursor()
    {
        if(followingCursor == false) 
        {
            InputController.OnMouseGridPosChanged += OnMouseGridPosChanged;

            followingCursor = true;
        }
    }
    public void StopFollowCursor()
    {
        if (followingCursor == true)
        {
            InputController.OnMouseGridPosChanged -= OnMouseGridPosChanged;

            followingCursor = false;
        }
    }
    protected virtual void HandleFollowCursorMove()
    {
        currentGridNode = InputController.MouseGridNode;

        if (currentGridNode != null)
        {
            PlaceObjectAtGridPos(currentGridNode);
            CreateVisuals();
            return;
        }

        DestroyVisuals();
        return;
    }
    #endregion

    #region Event Receivers
    private void OnMouseGridPosChanged()
    {
        if(followingCursor == true)
        {
           HandleFollowCursorMove();
        }
    }
    #endregion

    #region Overrides
    //We do not want to add these to the node for now, maybe in the future we will change this
    protected override void AddToGridNode()
    {

    }
    protected override void LeaveGridNode()
    {

    }
    protected override GameObject GetObjectModel()
    {
        return UIDataBase.GetUI(ui);
    }
    protected override void SetUpObject()
    {
        objType = ObjectTypeFilters.UI;
    }
    #endregion
}