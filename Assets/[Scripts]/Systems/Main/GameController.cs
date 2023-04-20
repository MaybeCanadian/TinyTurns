using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    #region Event Dispatchers
    public delegate void UpdateEvent(float delta);
    public static UpdateEvent OnUpdate;
    public static UpdateEvent OnFixedUpdate;
    public static UpdateEvent OnLateUpdate;
    #endregion

    public static GameController instance;

    [Header("Players")]
    public ObjectData objectData = null;
    public PlayerObjectData playerData;
    public PathfindingObjectData blueData;
    public int numObjects = 10;

    [Header("Controllers")]
    public PlayerController controller = null;

    [Header("Camera")]
    public PanSettings panSettings;

    private PathfindingObject testObject = null;

    #region Init Functions
    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            Init();
        }
    }
    private void Init()
    {
        InitSystems();
    }
    private void InitSystems()
    {
        EntityModelDataBase.OutSideInit();

        InputController.OutSideInit();

        MainCameraController.OutSideInit();
    }
    private void Start()
    {
        Grid grid = GridManager.GenerateMapGrid();

        Vector2Int start;

        for (int i = 0; i < numObjects; i++)
        {
            start = grid.GetRandomWalkableLocationOnGrid();

            Object obj = ObjectManager.CreateObject(objectData, start);
        }

        start = grid.GetRandomWalkableLocationOnGrid();

        PlayerObject playerObj = ObjectManager.CreatePlayerObject(playerData, start);

        controller = new PlayerController();

        MainCameraController.SetPanSettings(panSettings);
    }
    #endregion

    #region Update Functions
    private void Update()
    {
        float delta = Time.deltaTime;

        InputController.Update(delta);

        OnUpdate?.Invoke(delta);

        MainCameraController.Update(delta);
    }
    private void FixedUpdate()
    {
        float fixedDelta = Time.fixedDeltaTime;

        InputController.FixedUpdate(fixedDelta);

        OnFixedUpdate?.Invoke(fixedDelta);

        MainCameraController.FixedUpdate(fixedDelta);
    }
    private void LateUpdate()
    {
        float delta = Time.deltaTime;

        InputController.LateUpdate(delta);

        OnLateUpdate?.Invoke(delta);

        MainCameraController.LateUpdate(delta);
    }
    #endregion

    #region Debug
    private void MoveObjectToRandomLocation()
    {
        if(testObject == null)
        {
            Debug.Log("obj is null");
            return;
        }

        Grid grid = GridManager.GetMapGrid();

        if(grid == null)
        {
            Debug.Log("grid is null");
            return;
        }

        Vector2Int gridPos = grid.GetRandomWalkableLocationOnGrid();

        testObject.PlaceObjectAtGridPos(gridPos);

        Debug.Log("moved object to " + gridPos);
    }
    private void PathObjectToRandomLocation()
    {
        if (testObject == null)
        {
            Debug.Log("obj is null");
            return;
        }

        Grid grid = GridManager.GetMapGrid();

        if (grid == null)
        {
            Debug.Log("grid is null");
            return;
        }

        Vector2Int gridPos = grid.GetRandomWalkableLocationOnGrid();

        (testObject as PathfindingObject).PathToGridPosition(gridPos);

        Debug.Log("starting pathing object to " + gridPos);
    }
    #endregion
}
