using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController
{
    #region Event Dispatchers
    public delegate void PosseEvent(PlayerController controller, PlayerObject player);
    public PosseEvent OnPosse;
    public PosseEvent OnUnPosse;

    public delegate void MouseEvent();
    public MouseEvent OnMouseMoved;
    public MouseEvent OnGridNodeChanged;

    public delegate void ButtonEvent(CallBackContext<bool> context);
    public ButtonEvent OnButtonInput;

    public delegate void VectorEvent(CallBackContext<Vector2> context);
    public VectorEvent OnMoveInput;
    #endregion

    public static PlayerObject currentPlayer = null;

    public Vector3 currentMouseWorldPos = Vector3.zero;
    public GridNode currentMouseGridNode = null;

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
        MousePosCheck();

        MouseInputCheck();
    }
    private void FixedUpdate(float fixedDelta)
    {

    }
    private void LateUpdate(float delta)
    {

    } 
    #endregion

    #region Mouse
    private void MousePosCheck()
    {
        Vector2 mousePos = Input.mousePosition;

        Vector3 newMouseWorldPos = mainCamera.WorldToScreenPoint(mousePos);

        if (GridManager.GetGridPosFromWorldPos(newMouseWorldPos, out Vector2Int newGridPos))
        {
            if(currentMouseGridNode == null)
            {
                currentMouseGridNode = GridManager.GetGridNode(newGridPos.x, newGridPos.y);

                OnGridNodeChanged?.Invoke();
            }
            else
            {
                if(currentMouseGridNode.GetGridPos() != newGridPos)
                {
                    currentMouseGridNode = GridManager.GetGridNode(newGridPos.x, newGridPos.y);

                    OnGridNodeChanged?.Invoke();
                }
            }
        }

        if(newMouseWorldPos != currentMouseWorldPos)
        {
            OnMouseMoved?.Invoke();
        }
    }
    private void MouseInputCheck()
    {
        
    }
    private void GetButtonInputs()
    {

    }
    #endregion

    #region Player Possesion
    public void PossePlayer(PlayerObject player)
    {
        currentPlayer = player;
        OnPosse?.Invoke(this, player);

        currentPlayer.OnObjectRemoved += OnObjectRemoved;
    }
    public void UnPossePlayer()
    {
        if(currentPlayer == null)
        {
            return;
        }

        currentPlayer.OnObjectRemoved -= OnObjectRemoved;

        OnUnPosse?.Invoke(this, currentPlayer);

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
        UnPossePlayer();
    }
    #endregion
}

#region Input Context
public class CallBackContext<T>
{
    public T value;
    public ContextTypes context;
}
public enum ContextTypes
{
    Pressed,
    Held,
    Canceled
}
#endregion
