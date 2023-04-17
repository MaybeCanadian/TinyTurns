using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject : Object
{
    UIList ui;

    public UIObject(UIList uiType) : base(null)
    {
        ui = uiType;
    }

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
    #endregion
}