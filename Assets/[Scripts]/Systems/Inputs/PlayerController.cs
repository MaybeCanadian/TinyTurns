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
    private static void MouseInputCheck()
    {
       if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse");
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
