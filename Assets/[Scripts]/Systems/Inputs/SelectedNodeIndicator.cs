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
}
