using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerObject : PathfindingObject
{
    #region Event Dispatchers
    public delegate void PlayerPossesionEvent();
    public PlayerPossesionEvent OnPlayerPossesed;
    public PlayerPossesionEvent OnPlayerRePossesed;
    public PlayerPossesionEvent OnPlayerUnPossesed;
    #endregion

    [Header("Visuals")]
    public SelectedNodeIndicator movementSelector = null;

    [Header("Possesion")]
    public PlayerController controller = null;
    public bool ableToBePossed = true;

    #region Init Functions
    public PlayerObject(PlayerObjectData data) : base(data)
    {
        movementSelector = new SelectedNodeIndicator(null);
    }
    protected override void ConnectEvents()
    {
        base.ConnectEvents();
    }
    protected override void DisconnectEvents()
    {
        base.DisconnectEvents();
    }
    protected override void SetUpObject()
    {
        objType = ObjectTypeFilters.Player;
    }
    #endregion

    #region Possesion
    public bool TryPosse(PlayerController inputController)
    {
        if(controller == inputController)
        {
            Debug.Log("already possesed by this controller.");
            return false;
        }

        if(ableToBePossed)
        {
            if(controller != null)
            {
                controller.ActivePlayerStopPosse();
                OnPlayerRePossesed?.Invoke();

                controller = inputController;

                return true;
            }

            controller = inputController;

            OnPlayerPossesed?.Invoke();
            return true;
        }

        return false;
    }
    public void UnPosse()
    {
        controller = null;

        OnPlayerUnPossesed?.Invoke();
    }
    #endregion

    #region EventReceievers

    #region LifeCycle
    public override void DestroyObject()
    {
        base.DestroyObject();

        if(controller != null)
        {
            controller.ActivePlayerStopPosse();

            OnPlayerUnPossesed?.Invoke();
        }

        controller = null;


    }
    #endregion

    #endregion

    #region Node Input Receivers
    protected void OnMouseButtonDown()
    {

    }
    #endregion
}
