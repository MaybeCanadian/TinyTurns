using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    public GameObject controllerCameraFocus = null;
    public PlayerObject activePlayer = null;

    #region Init Functions
    public PlayerController()
    {
        Debug.Log("test");

        ConnectEvents();
    }

    private void ConnectEvents()
    {
        Debug.Log("Events Connected");

        InputController.OnMouseDown += OnMouseDown;
        InputController.OnMouseUp += OnMouseUp;
        InputController.OnMouseHeld += OnMouseHeld;

        GridNode.OnNodeMouseDown += OnGridNodeMouseDown;
    }
    private void DisconnectEvents()
    {
        InputController.OnMouseDown -= OnMouseDown;
        InputController.OnMouseUp -= OnMouseUp;
        InputController.OnMouseHeld -= OnMouseHeld;

        GridNode.OnNodeMouseDown -= OnGridNodeMouseDown;
    }
    #endregion

    #region Event Receivers
    private void OnMouseDown(int button)
    {

    }
    private void OnMouseUp(int button)
    {

    }
    private void OnMouseHeld(int button)
    {

    }
    private void OnGridNodeMouseDown(GridNode node, int button)
    {
        Debug.Log("player controller");

        if(button == 0)
        {
            if (activePlayer == null)
            {
                List<Object> objs = node.GetObjectsOnNode();

                AttempPosse(objs);
            }
        }
    }
    #endregion

    #region Input Handling

    #endregion

    #region Possesing
    private void AttempPosse(List<Object> objs)
    {
        foreach(Object obj in objs)
        {
            if(obj.objType == ObjectTypeFilters.Player)
            {
                if(obj is PlayerObject)
                {
                    if((obj as PlayerObject).TryPosse(this))
                    {
                        activePlayer = obj as PlayerObject;

                        Debug.Log("possesed");

                        return;
                    }
                }
            }
        }
    }
    public void ActivePlayerStopPosse()
    {
        activePlayer = null;
    }
    #endregion
}
