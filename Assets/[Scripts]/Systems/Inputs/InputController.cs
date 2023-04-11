using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public static class InputController
{
    #region Event Dispatchers
    public delegate void PosseEvent(PlayerObject player);
    public static PosseEvent OnPosse;
    public static PosseEvent OnUnPosse;

    public delegate void MousePositionEvent();
    public static MousePositionEvent OnMouseWorldChanged;
    public static MousePositionEvent OnMouseGridChanged;

    public delegate void MouseMoveEvent(GridNode node);
    public static MouseMoveEvent OnMouseEnter;
    public static MouseMoveEvent OnMouseExit;

    public delegate void MouseButtonEvent(int button);
    public static MouseButtonEvent OnMouseDown;

    //public delegate void ButtonEvent(InputAction<bool> context);
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
        if(GetCamera() == false)
        {
            return;
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
                currentMouseGridNode.OnMouseDown(0);

                OnMouseDown?.Invoke(0);
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            if(currentMouseGridNode != null)
            {
                currentMouseGridNode.OnMouseDown(1);

                OnMouseDown?.Invoke(0);
            }
        }
    }
    private static void GetButtonInputs()
    {

    }
    private static bool GetCamera()
    {
        if (mainCamera == null)
        { 
            mainCamera = Camera.main;

            if (mainCamera == null)
            {
                return false;
            }
        }

        return true;
    }
    #endregion

    #region Node Functions
    private static void ChangeCurrentGridNode(GridNode newNode)
    {
        if(currentMouseGridNode != null)
        {
            currentMouseGridNode.OnMouseExit();

            OnMouseExit?.Invoke(currentMouseGridNode);
        }

        currentMouseGridNode = newNode;

        if(currentMouseGridNode != null)
        {
            currentMouseGridNode.OnMouseEnter();

            OnMouseEnter?.Invoke(currentMouseGridNode);
        }

        OnMouseGridChanged?.Invoke();
    }
    private static void ChangeCurrentWorldPos(Vector3 newPos)
    {
        currentMouseWorldPos = newPos;

        OnMouseWorldChanged?.Invoke();
    }
    #endregion

    #region Player Possesion
    public static void PossePlayer(PlayerObject player)
    {
        currentPlayer = player;
        OnPosse?.Invoke(player);

        currentPlayer.OnObjectRemoved += OnObjectRemoved;

        currentPlayer.SetAsFollowTarget();
    }
    public static void UnPossePlayer()
    {
        if(currentPlayer == null)
        {
            return;
        }

        currentPlayer.OnObjectRemoved -= OnObjectRemoved;

        OnUnPosse?.Invoke(currentPlayer);

        CameraFollowScript.UnfollowTarget();

        currentPlayer = null;
    }
    private static void AttemptPosse()
    {
        if (currentMouseGridNode != null)
        {
            List<Object> objects = currentMouseGridNode.GetObjectsOnNode(ObjectTypeFilters.Player);

            if(objects.Count > 0)
            {
                foreach(Object obj in objects)
                {
                    if(obj is PlayerObject)
                    {
                        PossePlayer((PlayerObject)obj);
                    }
                }
            }
        }
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
