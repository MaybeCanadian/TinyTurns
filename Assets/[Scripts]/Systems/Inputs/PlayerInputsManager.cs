using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController
{
    #region Event Dispatchers
    public delegate void PosseEvent(PlayerObject player);
    public static PosseEvent OnPosse;
    public static PosseEvent OnUnPosse;
    #endregion

    public static PlayerObject currentPlayer = null;

    public Camera mainCamera;

    #region Init Functions
    public PlayerController()
    {
        mainCamera = Camera.main;

        ConnectEvents();
    }
    private void ConnectEvents()
    {
        GameController.OnUpdate += Update;
    }
    private void DisconnectEvents()
    {
        GameController.OnUpdate -= Update;
    }
    #endregion

    #region Update Functions
    private void Update(float delta)
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos =  mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    #endregion

    #region Player Possesion
    public void PossePlayer(PlayerObject player)
    {
        currentPlayer = player;
        OnPosse?.Invoke(player);

        currentPlayer.OnObjectRemoved += OnObjectRemoved;
    }
    public void UnPossePlayer()
    {
        if(currentPlayer == null)
        {
            return;
        }

        currentPlayer.OnObjectRemoved -= OnObjectRemoved;

        OnUnPosse?.Invoke(currentPlayer);

        currentPlayer = null;
    }
    #endregion

    #region Lifecycle
    public void DestroyController()
    {
        DisconnectEvents();

        mainCamera = null;
        currentPlayer = null;
    }
    #endregion

    #region Event Recievers
    private void OnObjectRemoved()
    {
        currentPlayer = null;
    }
    #endregion
}
