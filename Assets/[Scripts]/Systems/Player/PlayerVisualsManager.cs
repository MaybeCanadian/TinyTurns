using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualsManager : MonoBehaviour
{
    public static PlayerVisualsManager instance;

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
        }
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
    #endregion

    #region Event Recievers
    private void OnPlayerCreated(Player player)
    {

    }
    #endregion
}
