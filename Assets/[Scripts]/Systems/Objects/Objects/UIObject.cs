using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject : Object
{
    protected UIObjectType UIType;

    public UIObject(UIObjectType type) : base(null)
    {
        UIType = type;
    }

    protected override void SetUpObject()
    {
        objType = ObjectTypeFilters.UI;

        switch(UIType) {
            case UIObjectType.MovementIndicator:
                entityID = EntityList.MovementIndicator;
                objectName = "Movement Indicator";
                break;
            case UIObjectType.MovementPathIndicator:
                entityID = EntityList.MovementPath;
                objectName = "Path";
                break;
        }
    }
}

[System.Serializable]
public enum UIObjectType
{
    MovementIndicator,
    MovementPathIndicator
}
