using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    public Player connectedPlayer;

    private GameObject bodyObject;
    private SpriteRenderer bodyRenderer;
    private GameObject feetObject;
    private SpriteRenderer feetRenderer;

    #region Init Functions
    private void OnEnable()
    {
        if(connectedPlayer != null)
        {
            ConnectEvents();
        }
    }
    private void OnDisable()
    {
        if(connectedPlayer != null)
        {
            DisconnectEvents();
        }
    }
    public void ConnectToPlayer(Player player)
    {
        connectedPlayer = player;

        ConnectEvents();

        SetUpPlayerVisuals();
    }
    private void ConnectEvents()
    {
        if(connectedPlayer == null)
        {
            Debug.LogError("ERROR - Could not connect events as connected player is null.");
            return;
        }

        connectedPlayer.OnPlayerMoved += OnPlayerMoved;
    }
    private void DisconnectEvents()
    {
        if(connectedPlayer == null)
        {
            Debug.LogError("ERROR - Could not disconnect events as connected player is null.");
            return;
        }

        connectedPlayer.OnPlayerMoved -= OnPlayerMoved;
    }
    private void SetUpPlayerVisuals()
    {
        bodyObject = new GameObject();
        bodyObject.transform.parent = transform.parent;
        bodyRenderer = bodyObject.AddComponent<SpriteRenderer>();

        feetObject = new GameObject();
        feetObject.transform.parent = transform.parent;
        feetRenderer = feetObject.AddComponent<SpriteRenderer>();
    }
    #endregion

    #region Event Recievers
    private void OnPlayerMoved()
    {
        MoveToPlayerPos();
    }
    #endregion

    #region Player Movement
    private void MoveToPlayerPos()
    {
        if (connectedPlayer == null)
        {
            Debug.LogError("ERROR - Could not move to player as connected player is null.");
            return;
        }

        transform.position = connectedPlayer.worldPos;

        return;
    }
    #endregion

    #region Visuals
    private void ShowPlayerVisuals()
    {
        if(connectedPlayer == null)
        {
            Debug.LogError("ERROR - Could not show player visuals as connected player is null.");
            return;
        }


    }
    #endregion
}
