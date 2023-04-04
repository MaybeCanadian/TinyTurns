using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualsManager : MonoBehaviour
{
    public static PlayerVisualsManager instance;

    public GameObject playerParent = null;

    #region Init Functions
    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            Init();
        }
    }
    private void Init()
    {
        SetUpParent();
    }
    private void OnEnable()
    {
        ConnectEvents();
    }
    private void OnDisable()
    {
        DisconnectEvents();
    }
    private void ConnectEvents()
    {
        PlayerManager.OnPlayerCreated += OnPlayerCreated;
    }
    private void DisconnectEvents()
    {
        PlayerManager.OnPlayerCreated -= OnPlayerCreated;
    }
    private void SetUpParent()
    {
        playerParent = new GameObject();
        playerParent.name = "[Players]";
    }
    #endregion

    #region Event Recievers
    private void OnPlayerCreated(Player player)
    {
        GameObject playerVisualsOBJ = new GameObject();

        playerVisualsOBJ.name = player.playerName;

        playerVisualsOBJ.transform.parent = playerParent.transform;

        PlayerVisuals visuals = playerVisualsOBJ.AddComponent<PlayerVisuals>();

        visuals.ConnectToPlayer(player);
    }
    #endregion
}
