using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : PathfindingObject
{
    public static SelectedNodeIndicator movementSelector = null;
    public PlayerObject(PlayerObjectData data) : base(data)
    {
        movementSelector = new SelectedNodeIndicator(null);
    }
    private void MoveMovementIndicator(GridNode newNode)
    {
        if (movementSelector != null)
        {
            if (newNode != null)
            {
                movementSelector.PlaceObjectAtGridPos(newNode.GetGridPos());

                movementSelector.CreateVisuals();
            }
            else
            {
                movementSelector.DestroyVisuals();
            }
        }
    }
    public static void CreateMovementIndicatorVisuals()
    {
        if (movementSelector == null)
        {
            Debug.LogError("ERROR - Could not create movement indicator visuals as the node selector is null.");
            return;
        }

        movementSelector.CreateVisuals();
    }
    public static void DestroyMovementIndicatorVisuals()
    {
        if (movementSelector == null)
        {
            Debug.LogError("ERROR - Could not destroy movement indicator visuals as the node selector is null.");
            return;
        }

        movementSelector.DestroyVisuals();
    }
}
