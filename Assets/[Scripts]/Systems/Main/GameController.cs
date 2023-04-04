using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        GridManager.GenerateMapGrid();

        Grid grid = GridManager.GetMapGrid();

        GridNode startNode = grid.GetNode(0, 1);

        if(startNode == null)
        {
            Debug.Log("start");
        }

        GridNode endNode = grid.GetNode(0, 0);

        if(endNode == null)
        {
            Debug.Log("end");
        }

        if(PathfindingSystem.FindPathBetweenNodes(startNode, endNode, out PathRoute route))
        {
            route.DebugPrintSize();
            route.DebugPrintPath();
        }
    }
}
