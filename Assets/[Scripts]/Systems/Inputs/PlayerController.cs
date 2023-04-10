using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public static class PlayerController
{
    #region Event Dispatchers
    public delegate void PosseEvent(PlayerObject player);
    public static PosseEvent OnPosse;
    public static PosseEvent OnUnPosse;

    public delegate void MouseEvent();
    public static MouseEvent OnMouseMoved;
    public static MouseEvent OnGridNodeChanged;
    #endregion

    #region Player
    public static PlayerObject currentPlayer = null;
    #endregion

    public static Vector3 currentMouseWorldPos = Vector3.zero;
    public static GridNode currentMouseGridNode = null;

    static bool inited = false;

    private static Camera mainCamera;

    #region Init Functions
    public static void OutSideInit()
    {
        CheckInit();
    }
    private static void CheckInit()
    {
        if(inited == false)
        {
            Init();
        }
    }
    private static void Init()
    {
        inited = true;

        GetCamera();

        ConnectEvents();
    }
    private static void ConnectEvents()
    {
        GameController.OnUpdate += Update;
        GameController.OnFixedUpdate += FixedUpdate;
        GameController.OnLateUpdate += LateUpdate;
    }
    private static void DisconnectEvents()
    {
        GameController.OnUpdate -= Update;
        GameController.OnFixedUpdate -= FixedUpdate;
        GameController.OnLateUpdate -= LateUpdate;
    }
    #endregion

    #region Update Functions
    private static void Update(float delta)
    {
        if(mainCamera == null)
        {
            GetCamera();

            if(mainCamera == null)
            {
                return;
            }
        }

        MousePosCheck();

        MouseInputCheck();

        GetButtonInputs();
    }
    private static void FixedUpdate(float fixedDelta)
    {

    }
    private static void LateUpdate(float delta)
    {

    } 
    #endregion

    #region Input
    private static void MousePosCheck()
    {
        Vector2 mousePos = Input.mousePosition;

        Vector3 newMouseWorldPos = mainCamera.ScreenToWorldPoint(mousePos);

        if (GridManager.GetGridPosFromWorldPos(newMouseWorldPos, out Vector2Int newGridPos))
        {
            if(currentMouseGridNode == null)
            {
                ChangeCurrentGridNode(GridManager.GetGridNode(newGridPos.x, newGridPos.y));
            }
            else
            {
                if(currentMouseGridNode.GetGridPos() != newGridPos)
                {
                    ChangeCurrentGridNode(GridManager.GetGridNode(newGridPos.x, newGridPos.y));
                }
            }
        }
        else
        {
            ChangeCurrentGridNode(null);
        }

        if(newMouseWorldPos != currentMouseWorldPos)
        {
            ChangeCurrentWorldPos(newMouseWorldPos);
        }
    }
    private static void MouseInputCheck()
    {
       if(Input.GetMouseButtonDown(0))
        {
            if (currentMouseGridNode != null)
            {
                if(currentPlayer != null)
                {
                    currentPlayer.PathToGridPosition(currentMouseGridNode.GetGridPos());
                }      
            }
        }
    }
    private static void GetButtonInputs()
    {

    }
    private static void GetCamera()
    {
        mainCamera = Camera.main;
    }
    #endregion

    #region Node Functions
    private static void ChangeCurrentGridNode(GridNode newNode)
    {
        currentMouseGridNode = newNode;

        OnGridNodeChanged?.Invoke();
    }
    private static void ChangeCurrentWorldPos(Vector3 newPos)
    {
        currentMouseWorldPos = newPos;

        OnMouseMoved?.Invoke();
    }
    #endregion

    #region Player Possesion
    public static void PossePlayer(PlayerObject player)
    {
        currentPlayer = player;
        OnPosse?.Invoke(player);

        currentPlayer.OnObjectRemoved += OnObjectRemoved;
    }
    public static void UnPossePlayer()
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
    public static void DestroyController()
    {
        DisconnectEvents();

        mainCamera = null;
        currentPlayer = null;
    }
    #endregion

    #region Event Recievers
    private static void OnObjectRemoved()
    {
        UnPossePlayer();
    }
    #endregion
}

public class InputAction<T>
{
    public delegate void InputEvent();
    public InputEvent Pressed;
    public InputEvent Released;

    public InputStates state;

    public KeyCode actionKey;

    public InputAction(KeyCode key)
    {
        state = InputStates.UnPressed;
        actionKey = key;
    }

    public void CheckInput()
    {
        if(Input.GetKeyDown(actionKey))
        {
            state = InputStates.Started;

            Pressed?.Invoke();
        }
    }
}

public enum InputStates
{
    UnPressed,
    Started,
    Held,
    Canceled
}
