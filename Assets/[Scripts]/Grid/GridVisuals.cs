using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisuals : MonoBehaviour
{
    public static GridVisuals instance;

    public GridVisualNode[,] nodes;
    public static Dictionary<GridTileTypes, Sprite> tileSpriteDict = null;

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
            CheckInit();
        }
    }
    private static void CheckInit()
    {
        if(tileSpriteDict == null)
        {
            Init();
        }
    }
    private static void Init()
    {
        LoadTileAssets();
    }
    private static void LoadTileAssets()
    {
        tileSpriteDict = new Dictionary<GridTileTypes, Sprite>();

        foreach(int tileName in Enum.GetValues(typeof(GridTileTypes)))
        {
            if((GridTileTypes)tileName == GridTileTypes.NULL)
            {
                continue;
            }

            Sprite tileSprite = Resources.Load<Sprite>("Tiles/" + ((GridTileTypes)tileName).ToString());

            if(tileSprite == null)
            {
                Debug.LogError("ERROR - Could not load the tilem sprite for tile " + (GridTileTypes)tileName);
                continue;
            }

            tileSpriteDict.Add((GridTileTypes)tileName, tileSprite);
        }
    }
    #endregion

    #region Visuals Functions
    /// <summary>
    /// Creates the visuals that correspond to the created grid. If the grid is not created this function will fail.
    /// </summary>
    public void CreateTileVisuals()
    {
        Grid grid = GridManager.GetGrid();

        if (grid == null)
        {
            Debug.LogError("ERROR - Could not create visuals as the grid is not set up yet.");
            return;
        }

        nodes = new GridVisualNode[grid.gridX, grid.gridY];

        for(int x = 0; x < grid.gridX; x++)
        {
            for(int y = 0; y < grid.gridY; y++)
            {
                nodes[x, y] = new GridVisualNode();


            }
        }
    }
    #endregion

    #region Tile Assets
    /// <summary>
    /// Returns the loaded sprite for the given tile tyle key. Will return null if key does not have a match.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static Sprite GetTileAsset(GridTileTypes type)
    {
        CheckInit();

        if(type == GridTileTypes.NULL)
        {
            Debug.LogError("ERROR - No Tile exists for null tile type. This type is for debug only.");
            return null;
        }

        if(!tileSpriteDict.ContainsKey(type))
        {
            Debug.LogError("ERROR - The tile sprite dictionary does not have a sprite for the given tile type.");
            return null;
        }

        return tileSpriteDict[type];
    }
    #endregion
}
