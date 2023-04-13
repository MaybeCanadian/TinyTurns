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
    public PlayerObjectData playerData;
    public PathfindingObjectData blueData;
    public int numObjects = 10;

    [Header("Controllers")]
    public PlayerController controller = null;

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
    }
    private void Start()
    {
        Grid grid = GridManager.GenerateMapGrid();

        for (int i = 0; i < numObjects; i++)
        {
            Vector2Int start = grid.GetRandomWalkableLocationOnGrid();

            PlayerObject obj = ObjectManager.CreatePlayerObject(playerData, start);
        }

        controller = new PlayerController();
    }
    #endregion

    #region Update Functions
    private void Update()
    {
        InputController.Update(Time.deltaTime);

        OnUpdate?.Invoke(Time.deltaTime);
    }
    private void FixedUpdate()
    {
        InputController.FixedUpdate(Time.fixedDeltaTime);

        OnFixedUpdate?.Invoke(Time.fixedDeltaTime);
    }
    private void LateUpdate()
    {
        InputController.LateUpdate(Time.deltaTime);

        OnLateUpdate?.Invoke(Time.deltaTime);
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
