using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public static class InputController
{
    #region Evetnt Dispatchers
    public delegate void MousePositionEvent();
    public static MousePositionEvent OnMouseWorldChanged;
    public static MousePositionEvent OnMouseGridChanged;

    public delegate void MouseMoveEvent(GridNode node);
    public static MouseMoveEvent OnMouseEnter;
    public static MouseMoveEvent OnMouseExit;
    public static MouseMoveEvent OnMouseOver;

    public delegate void MouseButtonEvent(int button);
    public static MouseButtonEvent OnMouseDown;
    public static MouseButtonEvent OnMouseHeld;
    public static MouseButtonEvent OnMouseUp;
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

        if (Input.mousePresent)
        {
            MousePosCheck();
            MouseInputCheck();
        }

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
                else
                {
                    currentMouseGridNode.OnMouseOver();

                    OnMouseOver?.Invoke(currentMouseGridNode);
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
        for (int i = 0; i < 3; i++)
        {
            if (Input.GetMouseButtonDown(i))
            {
                currentMouseGridNode?.OnMouseDown(i);

                OnMouseDown?.Invoke(i);
            }
            if (Input.GetMouseButtonUp(i))
            {
                currentMouseGridNode?.OnMouseUp(i);

                OnMouseUp?.Invoke(i);
            }
            if (Input.GetMouseButton(i))
            {
                currentMouseGridNode?.OnMouseHeld(i);

                OnMouseHeld?.Invoke(i);
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

    #region Lifecycle
    public static void DestroyController()
    {
        DisconnectEvents();

        mainCamera = null;
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
