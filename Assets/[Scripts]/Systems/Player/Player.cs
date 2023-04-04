using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    #region Event Dispatchers
    public delegate void PlayerMoveEvent();
    public PlayerMoveEvent OnPlayerMove;
    #endregion

    public Vector3Int gridPos;
    public Vector3 worldPos;

    private GameObject playerOBJ = null;

    public Player(Vector3Int startPos)
    {
        this.gridPos = startPos;

        Grid grid = GridManager.GetMapGrid();

        if(grid != null)
        {
            GridNode node = grid.GetNode(gridPos.x, gridPos.y);

            worldPos = node.GetWorldPos();
        }

        CreatePlayerOBJ();
    }
    private void CreatePlayerOBJ()
    {
        playerOBJ = new GameObject();
        playerOBJ.name = "Player";

        playerOBJ.transform.position = worldPos;
    }
}
