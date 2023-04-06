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

        ObjectManager.CreateObject(testObjectData, testObjectStart);
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
}
