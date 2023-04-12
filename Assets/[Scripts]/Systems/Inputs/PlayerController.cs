using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController
{
    public GameObject controllerCameraFocus = null;
    public PlayerObject activePlayer = null;

    #region Init Functions
    public PlayerController()
    {
        ConnectEvents();
    }
    ~PlayerController()
    {
        DisconnectEvents();
    }
    private void ConnectEvents()
    {
        InputController.OnMouseDown += OnMouseDown;
        InputController.OnMouseUp += OnMouseUp;
        InputController.OnMouseHeld += OnMouseHeld;

        GridNode.OnGridNodeClicked += OnGridNodeClicked;
    }
    private void DisconnectEvents()
    {
        InputController.OnMouseDown -= OnMouseDown;
        InputController.OnMouseUp -= OnMouseUp;
        InputController.OnMouseHeld -= OnMouseHeld;

        GridNode.OnGridNodeClicked -= OnGridNodeClicked;
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
    private void OnGridNodeClicked(GridNode node, int button)
    {
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
