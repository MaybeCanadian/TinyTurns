using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    private Player connectedPlayer;

    private GameObject PlayerObject = null;

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

        ShowPlayerVisuals();

        MoveToPlayerPos();
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
    public void ShowPlayerVisuals()
    {
        if(connectedPlayer == null)
        {
            Debug.LogError("ERROR - Could not show player visuals as connected player is null.");
            return;
        }

        GameObject playerModel = EntityModelDataBase.GetModel(EntityList.PlayerOrange);

        if(playerModel == null)
        {
            Debug.LogError("ERROR - Could not show player visuals as player model is null.");
            return;
        }

        PlayerObject = Instantiate(playerModel, transform);

        PlayerObject.transform.position = Vector3.zero;

        PlayerObject.name = (EntityList.PlayerOrange).ToString();
    }
    public void DestroyVisuals()
    {
        if(PlayerObject == null)
        {
            return;
        }

        Destroy(PlayerObject);
    }
    #endregion
}
