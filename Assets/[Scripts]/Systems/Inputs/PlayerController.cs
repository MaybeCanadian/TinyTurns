using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerController
{
    public static GameObject controllerCameraFocus = null;

    private static bool inited = false;

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
        ConnectEvents();

        inited = true;
    }
    private static void ConnectEvents()
    {
        InputController.OnMouseDown += OnMouseDown;
        InputController.OnMouseUp += OnMouseUp;
        InputController.OnMouseHeld += OnMouseHeld;


    }
    private static void DisconnectEvents()
    {
        InputController.OnMouseDown -= OnMouseDown;
        InputController.OnMouseUp -= OnMouseUp;
        InputController.OnMouseHeld -= OnMouseHeld;
    }
    #endregion

    #region Event Receivers
    private static void OnMouseDown(int button)
    {

    }
    private static void OnMouseUp(int button)
    {

    }
    private static void OnMouseHeld(int button)
    {

    }
    #endregion
}
