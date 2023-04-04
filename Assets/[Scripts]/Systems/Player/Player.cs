using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    #region Event Dispatchers
    public delegate void PlayerEvent();
    public PlayerEvent OnPlayerMoved;
    #endregion

    public Vector2Int gridPos;
    public Vector3 worldPos;

    #region Init Functions
    public Player(Vector2Int startGridPos)
    {
        this.gridPos = startGridPos;

        DetermineWorldPos();

        ConnectEvents();
    }
    private void ConnectEvents()
    {
        GameController.OnUpdate += Update;
        GameController.OnFixedUpdate += FixedUpdate;
        GameController.OnLateUpdate += LateUpdate;
    }
    private void DisconnectEvents()
    {
        GameController.OnUpdate -= Update;
        GameController.OnFixedUpdate -= FixedUpdate;
        GameController.OnLateUpdate -= LateUpdate;
    }
    #endregion

    #region Update Functions
    private void Update(float delta)
    {

    }
    private void FixedUpdate(float fixedDelta)
    {

    }
    private void LateUpdate(float delta)
    {

    }
    #endregion

    #region Movement
    private void DetermineWorldPos()
    {
        Grid grid = GridManager.GetMapGrid();

        if(grid == null)
        {
            return;
        }

        GridNode node = grid.GetNode(gridPos.x, gridPos.y);

        if(node == null)
        {
            return;
        }

        worldPos = node.GetWorldPos();
    }
    #endregion
}
