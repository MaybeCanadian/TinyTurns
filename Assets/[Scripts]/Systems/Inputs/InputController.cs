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

    #region Mouse
    public delegate void MousePositionEvent();
    public static MousePositionEvent OnMouseScreenPosChanged;
    public static MousePositionEvent OnMouseWorldPosChanged;
    public static MousePositionEvent OnMouseGridPosChanged;
    public static MousePositionEvent OnMouseViewPortPosChanged;

    public delegate void MouseButtonEvent(int button);
    public static MouseButtonEvent OnMouseDown;
    public static MouseButtonEvent OnMouseHeld;
    public static MouseButtonEvent OnMouseUp;
    #endregion

    #region Actions
    public delegate void ButtonEvents(InputAction<bool> context);
    public static ButtonEvents OnCameraCenterButtonPressed;

    public delegate void ActionEvents(InputAction<Vector2> context);
    public static ActionEvents OnCameraMoveAction;
    #endregion

    #endregion

    #region Mouse Positions
    public static Vector2 mouseScreenPos { get; private set; } = Vector2.zero;
    public static Vector3 mouseWorldPos { get; private set; } = Vector3.zero;
    public static Vector2 mouseViewPortPos { get; private set; } = Vector2.zero;
    public static GridNode MouseGridNode { get; private set; } = null;
    #endregion

    static bool inited = false;

    private static Camera mainCamera = null;

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
    }
    private static void SetUpAcitons()
    {

    }
    #endregion

    #region Update Functions
    public static void Update(float delta)
    {
        if(GetCamera() == false)
        {
            return;
        }

        if (Input.mousePresent)
        {
            HandleMouse();
        }

        GetButtonInputs();
    }
    public static void FixedUpdate(float fixedDelta)
    {

    }
    public static void LateUpdate(float delta)
    {

    }
    #endregion

    #region Mouse
    private static void HandleMouse()
    {
        MousePosCheck();
        MouseInputCheck();
    }

    #region Position
    private static void MousePosCheck()
    {
        CheckMouseScreenPos();

        CheckMouseWorldPos();

        CheckMouseGridPos();

        CheckMouseViewPortPos();
    }
    private static void CheckMouseScreenPos()
    {
        Vector2 screenPos = Input.mousePosition;
        
        if(screenPos != mouseScreenPos)
        {
            mouseScreenPos = screenPos;

            OnMouseScreenPosChanged?.Invoke();
        }
    }
    private static void CheckMouseWorldPos()
    {
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        if(worldPos != mouseWorldPos)
        {
            mouseWorldPos = worldPos;

            OnMouseWorldPosChanged?.Invoke();
        }
    }
    private static void CheckMouseGridPos()
    {
        GridNode newNode = GridManager.GetGridNodeFromWorldPos(mouseWorldPos);

        if(newNode == MouseGridNode)
        {
            if(MouseGridNode != null)
            {
                MouseGridNode.OnMouseStay();
            }

            return;
        }
        
        ChangeCurrentGridNode(newNode);
        return;
    }
    private static void CheckMouseViewPortPos()
    {
        Vector2 viewPortPos = mainCamera.ScreenToViewportPoint(mouseScreenPos);

        if(viewPortPos != mouseViewPortPos)
        {
            mouseViewPortPos = viewPortPos;

            OnMouseViewPortPosChanged?.Invoke();
        }
    }
    #endregion

    #region Input
    private static void MouseInputCheck()
    {
        for (int i = 0; i < 3; i++)
        {
            if (Input.GetMouseButtonDown(i))
            {
                MouseGridNode?.OnMouseDown(i);

                OnMouseDown?.Invoke(i);
            }
            if (Input.GetMouseButtonUp(i))
            {
                MouseGridNode?.OnMouseUp(i);

                OnMouseUp?.Invoke(i);
            }
            if (Input.GetMouseButton(i))
            {
                MouseGridNode?.OnMouseHeld(i);

                OnMouseHeld?.Invoke(i);
            }
        }
    }
    #endregion

    #endregion

    #region Input
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
        if(MouseGridNode != null)
        {
            MouseGridNode.OnMouseExit();
        }

        MouseGridNode = newNode;

        if(MouseGridNode != null)
        {
            MouseGridNode.OnMouseEnter();
        }

        OnMouseGridPosChanged?.Invoke();
    }
    #endregion

    #region Lifecycle
    public static void DestroyController()
    {
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
