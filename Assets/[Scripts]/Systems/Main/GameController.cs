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

    public ObjectData testObjectData;
    public Vector2Int testObjectStart;

    public float moveRate = 2.0f;

    private Object testObject = null;

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
        GridManager.GenerateMapGrid();

        testObject = ObjectManager.CreateObject(testObjectData, testObjectStart);

        InvokeRepeating("MoveObjectToRandomLocation", moveRate, moveRate);
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
    #endregion
}
