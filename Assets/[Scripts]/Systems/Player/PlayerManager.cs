using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager
{
    #region Event Dispatchers
    public delegate void PlayerEvent(Player player);
    public static PlayerEvent OnPlayerCreated;
    #endregion

    public static List<Player> playersList = null;

    #region Init Functions
    public static void OutSideInit()
    {
        CheckInit();

        return;
    }
    private static void CheckInit()
    {
        if(playersList == null)
        {
            Init();
        }

        return;
    }
    private static void Init()
    {
        playersList = new List<Player>();

        Debug.Log("Player Manager Init.");

        return;
    }
    #endregion

    #region Player Functions
    public static void CreatePlayer(Vector2Int startPos)
    {
        CheckInit();

        Player newPlayer = new Player(startPos);

        playersList.Add(newPlayer);

        OnPlayerCreated?.Invoke(newPlayer);
    }
    #endregion
}
