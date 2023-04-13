using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MainCameraController
{
    private static Camera mainCamera;

    #region Init Functions
    public static void OutSideInit()
    {
        CheckInit();
    }
    private static void CheckInit()
    {
        if(mainCamera == null)
        {
            Init();
        }
    }
    private static void Init()
    {
        GetCamera();

        //ConnectEvents();
    }
    private static void ConnectEvents()
    {
        //GameController.OnUpdate += Update;
        //GameController.OnFixedUpdate += FixedUpdate;
        //GameController.OnLateUpdate += LateUpdate;
    }
    private static void DisconnectEvents()
    {
        //GameController.OnUpdate -= Update;
        //GameController.OnFixedUpdate -= FixedUpdate;
        //GameController.OnLateUpdate -= LateUpdate;
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

    }
    #endregion

    #region Camera Move Functions

    #endregion

    #region Camera Functions
    private static void GetCamera()
    {
        if(mainCamera == null ) 
        {
            mainCamera = Camera.main;        
        }
    }
    #endregion
}
