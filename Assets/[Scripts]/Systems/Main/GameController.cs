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

    public PathfindingObjectData orangeData;
    public PathfindingObjectData blueData;
    public int numObjects = 10;

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
    }
    private void Start()
    {
        Grid grid = GridManager.GenerateMapGrid();

        Vector2Int start = grid.GetRandomWalkableLocationOnGrid();

        Object obj = ObjectManager.CreatePathfindingObject(orangeData, start);

        obj.SetAsFollowTarget();

        for (int i = 0; i < numObjects; i++)
        {
            start = grid.GetRandomWalkableLocationOnGrid();

            obj = ObjectManager.CreatePathfindingObject(blueData, start);
        }

    }
    #endregion

    #region Update Functions
    private void Update()
    {
        OnUpdate?.Invoke(Time.deltaTime);
    }
    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke(Time.fixedDeltaTime);
    }
    private void LateUpdate()
    {
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
