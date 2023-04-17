using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject : Object
{
    UIList ui;
    bool followingCursor = false;
    Object objAttachedTo = null;

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

        PlaceObjectAtGridPos(currentGridNode);
        CreateVisuals();
        return;
    }
    public void AttachToObject(Object obj)
    {
        if (obj == null)
        {
            return;
        }

        objAttachedTo = obj;

        obj.OnObjectWorldPosChanged += OnAttachedOBJWorldPosChanged;
        obj.OnObjectRemoved += OnAttachedOBJRemoved;

        PlaceObjectAtGridPos(obj.gridPos);
    }
    public void RemoveFromObject() 
    {
        if (objAttachedTo == null)
        {
            return;
        }

        objAttachedTo.OnObjectWorldPosChanged += OnAttachedOBJWorldPosChanged;
        objAttachedTo.OnObjectRemoved += OnAttachedOBJRemoved;

        objAttachedTo = null;
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
    private void OnAttachedOBJWorldPosChanged()
    {
        if(objAttachedTo == null)
        {
            return;
        }

        worldPos = objAttachedTo.worldPos;
        MoveObjToPosition();
    }
    private void OnAttachedOBJRemoved()
    {
        RemoveFromObject();
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