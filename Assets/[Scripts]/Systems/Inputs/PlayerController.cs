using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    public PlayerObject activePlayer = null;
    public UIObject activePlayerIcon = null;

    private bool currentlyDragging = false;

    #region Init Functions
    public PlayerController()
    {
        activePlayerIcon = new UIObject(UIList.ActivePlayer);
        ConnectEvents();
    }

    private void ConnectEvents()
    {
        InputController.OnMouseDown += OnMouseDown;
        InputController.OnMouseUp += OnMouseUp;
        InputController.OnMouseHeld += OnMouseHeld;

        GridNode.OnNodeMouseDown += OnGridNodeMouseDown;

        InputController.damageEvent += OnDamageEvent;
    }
    private void DisconnectEvents()
    {
        InputController.OnMouseDown -= OnMouseDown;
        InputController.OnMouseUp -= OnMouseUp;
        InputController.OnMouseHeld -= OnMouseHeld;

        GridNode.OnNodeMouseDown -= OnGridNodeMouseDown;

        InputController.damageEvent -= OnDamageEvent;   
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
        if(button == 0)
        {
            if(currentlyDragging == true)
            {
                activePlayer.MovementIndicatorStopFollowCursor();
                activePlayer.PathToMovementIndicator();
            }
        }
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
    private void OnDamageEvent()
    {
        if(activePlayer != null)
        {
            DamageStruct testDamage;
            testDamage.amount = 1;
            testDamage.damageType = DamageTypes.UNTYPED;
            testDamage.targetType = TargetTypes.UNTYPED;
            testDamage.source = null;

            activePlayer.TakeDamage(testDamage);
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

        if(activePlayer.currentGridNode == node)
        {
            currentlyDragging = true;

            activePlayer.ShowMovementIndicator();
        }
    }
    #endregion

    #region Possesing
    private void AttempPosse(List<Object> objs)
    {
        foreach(Object obj in objs)
        {
            Debug.Log("Player");
            if(obj.objType == ObjectTypeFilters.Player)
            {
                if(obj is PlayerObject)
                {
                    if((obj as PlayerObject).TryPosse(this))
                    {
                        activePlayer = obj as PlayerObject;
                        activePlayerIcon.AttachToObject(activePlayer);
                        activePlayerIcon.CreateVisuals();
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
            activePlayerIcon.RemoveFromObject();
            activePlayerIcon.DestroyVisuals();
            activePlayer.UnPosse();
            activePlayer = null;
        }
    }
    public void ActivePlayerStopPosse()
    {
        if (activePlayer != null)
        {
            activePlayerIcon.RemoveFromObject();
            activePlayerIcon.DestroyVisuals();
        }
        activePlayer = null;
    }
    #endregion
}
