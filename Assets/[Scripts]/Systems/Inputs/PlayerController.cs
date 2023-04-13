using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        if(button == 0)
        {

        }
        else if(button == 1)
        {
            UnPossePlayer();
        }
        else
        {

        }
    }
    private void OnMouseUp(int button)
    {

    }
    private void OnMouseHeld(int button)
    {

    }
    private void OnGridNodeMouseDown(GridNode node, int button)
    {
        if(button == 0)
        {
            HandleNodeClickedMainMouse(node);
        }
    }
    #endregion

    #region Input Handling
    private void HandleNodeClickedMainMouse(GridNode node)
    {
        if (activePlayer == null)
        {
            List<Object> objs = node.GetObjectsOnNode();

            AttempPosse(objs);

            return;
        }

        if(activePlayer.currentGridNode != node)
        {
            activePlayer.PathToGridPosition(node);
        }
    }

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

                        //Debug.Log("possesed");

                        return;
                    }
                }
            }
        }
    }
    private void UnPossePlayer()
    {
        if(activePlayer != null)
        {
            activePlayer.UnPosse();

            activePlayer = null;
        }
    }
    public void ActivePlayerStopPosse()
    {
        activePlayer = null;
    }
    #endregion
}
