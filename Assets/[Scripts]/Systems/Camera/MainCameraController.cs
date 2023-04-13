using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MainCameraController
{
    private static Camera mainCamera = null;

    private static bool hasTarget = false;
    private static Vector3 targetPos = Vector3.zero;

    [Header("Pan")]
    private static PanSettings panSettings;

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

        LoadDefaultPanSettings();

        ConnectEvents();

        inited = true;
    }
    private static void ConnectEvents()
    {
        //InputController.OnMouseViewPortPosChanged += OnMouseViewPortPosChanged;
    }
    private static void DisconnectEvents()
    {
        //InputController.OnMouseViewPortPosChanged -= OnMouseViewPortPosChanged;
    }
    private static void LoadDefaultPanSettings()
    {
        panSettings = new PanSettings();

        panSettings.minPan = new Vector2(0.1f, 0.1f);
        panSettings.maxPan = new Vector2(0.9f, 0.9f);

        panSettings.minSpeed = new Vector2(1.0f, 1.0f);
        panSettings.maxSpeed = new Vector2(5.0f, 5.0f);
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
        PanCamera(delta);
    }
    #endregion

    #region Event Receivers

    #endregion

    #region Pan
    private static void PanCamera(float delta)
    {
        if (!GetCamera())
        {
            return;
        }

        Vector2 viewPortPos = InputController.mouseViewPortPos;

        Vector3 panDirection = Vector3.zero;

        panDirection.x += CheckPanRight(viewPortPos);
        panDirection.x += CheckPanLeft(viewPortPos);

        panDirection.y += CheckPanUp(viewPortPos);
        panDirection.y += CheckPanDown(viewPortPos);

        mainCamera.transform.position += panDirection * delta;
    }
    private static float CheckPanUp(Vector2 viewPortPos)
    {
        if (viewPortPos.y > panSettings.maxPan.y)
        {
            float dif = viewPortPos.y - panSettings.maxPan.y;

            float total = 1.0f - panSettings.maxPan.y;

            if (total <= 0.0f)
            {
                return panSettings.maxSpeed.y;
            }

            float percent;

            if (panSettings.panMode == PanMode.Exponential)
            {
                percent = (dif * dif) / (total * total);
            }
            else
            {
                percent = dif / total;
            }

            percent = Mathf.Clamp(percent, 0.0f, 1.0f);

            return percent * (panSettings.maxSpeed.y - panSettings.minSpeed.y);
        }

        return 0.0f;
    }
    private static float CheckPanDown(Vector2 viewPortPos)
    {
        if (viewPortPos.y < panSettings.minPan.y)
        {
            float dif = panSettings.minPan.y - viewPortPos.y;

            float total = panSettings.minPan.y;

            if (total <= 0.0f)
            {
                return -panSettings.maxSpeed.y;
            }

            float percent;

            if (panSettings.panMode == PanMode.Exponential)
            {
                percent = (dif * dif) / (total * total);
            }
            else
            {
                percent = dif / total;
            }

            percent = Mathf.Clamp(percent, 0.0f, 1.0f);

            return -percent * (panSettings.maxSpeed.y - panSettings.minSpeed.y);
        }

        return 0.0f;
    }
    private static float CheckPanLeft(Vector2 viewPortPos)
    {
        if (viewPortPos.x < panSettings.minPan.x)
        {
            float dif = panSettings.minPan.x - viewPortPos.x;

            float total = panSettings.minPan.x;

            if (total <= 0.0f)
            {
                return -panSettings.maxSpeed.x;
            }

            float percent;

            if (panSettings.panMode == PanMode.Exponential)
            {
                percent = (dif * dif) / (total * total);
            }
            else
            {
                percent = dif / total;
            }

            percent = Mathf.Clamp(percent, 0.0f, 1.0f);

            return -percent * (panSettings.maxSpeed.x - panSettings.minSpeed.x);
        }

        return 0.0f;
    }
    private static float CheckPanRight(Vector2 viewPortPos)
    {
        if (viewPortPos.x > panSettings.maxPan.x)
        {
            float dif = viewPortPos.x - panSettings.maxPan.x;

            float total = 1.0f - panSettings.maxPan.x;

            if (total <= 0.0f)
            {
                return panSettings.maxSpeed.x;
            }

            float percent;

            if (panSettings.panMode == PanMode.Exponential)
            {
                percent = (dif * dif) / (total * total);
            }
            else
            {
                percent = dif / total;
            }

            percent = Mathf.Clamp(percent, 0.0f, 1.0f);

            return percent * (panSettings.maxSpeed.x - panSettings.minSpeed.x);
        }

        return 0.0f;
    }
    public static void SetPanSettings(PanSettings settings)
    {
        panSettings = settings;
    } 
    #endregion

    #region Camera Move Functions
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

[System.Serializable]
public struct PanSettings
{
    public Vector2 minPan;
    public Vector2 maxPan;

    public Vector2 minSpeed;
    public Vector2 maxSpeed;

    public PanMode panMode;
}

[System.Serializable]
public enum PanMode
{
    Linear,
    Exponential
}