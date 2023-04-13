using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MainCameraController
{
    private static Camera mainCamera = null;

    private static bool hasTarget = false;
    private static Vector3 targetPos = Vector3.zero;

    //private static float cameraPanSpeed = 0.0f;

    private static Vector2 maxCam = new Vector2(0.9f, 0.9f);
    private static Vector2 minCam = new Vector2(0.1f, 0.1f);
    private static float panSpeed = 1.0f;
    private static Vector3 panDirection = Vector2.zero;

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
        GetCamera();

        ConnectEvents();

        inited = true;
    }
    private static void ConnectEvents()
    {
        InputController.OnMouseViewPortPosChanged += OnMouseViewPortPosChanged;
    }
    private static void DisconnectEvents()
    {
        InputController.OnMouseViewPortPosChanged -= OnMouseViewPortPosChanged;
    }
    #endregion

    #region Update Functions
    public static void Update(float delta)
    {
        
    }
    public static void FixedUpdate(float fixedDelta)
    {
        
    }
    public static void LateUpdate(float delta)
    {
        //MoveCamera();

        PanCamera(delta);
    }
    #endregion

    #region Event Receivers
    private static void OnMouseViewPortPosChanged()
    {
        Vector2 viewPortPos = InputController.mouseViewPortPos;

        panDirection = Vector3.zero;

        Debug.Log(viewPortPos);

        if(viewPortPos.x > maxCam.x)
        {
            panDirection.x += 1;
        }
        else if(viewPortPos.x < minCam.x)
        {
            panDirection.x -= 1;
        }

        if(viewPortPos.y > maxCam.y)
        {
            panDirection.y += 1;
        }
        else if(viewPortPos.y < minCam.y)
        {
            panDirection.y -= 1;
        }

        panDirection = panDirection.normalized;
    }
    #endregion

    #region Camera Move Functions
    private static void PanCamera(float delta)
    {
        if(!GetCamera())
        {
            return;
        }

        mainCamera.transform.position += panDirection * panSpeed * delta;
    }
    public static void MoveCameraToWorldPos(Vector3 worldPos, float panSpeed = 0.0f, bool teleport = true)
    {
        if(!GetCamera())
        {
            Debug.LogError("ERROR - Could not move the camera as the camera could not be gotten.");
            return;
        }

        if(teleport == true)
        {

        }

    }
    private static void MoveCamera()
    {
        if(hasTarget == true)
        {

        }
    }
    #endregion

    #region Camera Functions
    private static bool GetCamera()
    {
        if(mainCamera == null ) 
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
}
