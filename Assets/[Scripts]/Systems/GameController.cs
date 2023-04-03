using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public int gridX = 10;
    public int gridY = 10;
    public float gridSizeX = 1.0f;
    public float gridSizeY = 1.0f; 

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
            DontDestroyOnLoad(gameObject);

            Init();
        }
    }
    private void Init()
    {
        GridManager.CreateGrid(gridX, gridY, gridSizeX, gridSizeY);
    }
    #endregion
}
