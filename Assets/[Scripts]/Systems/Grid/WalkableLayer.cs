using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkableLayer : MonoBehaviour
{
    static Tilemap walkableTileMap = null;

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        walkableTileMap = GetComponent<Tilemap>();
    }
    public static Tilemap GetWalkableTileMap()
    {
        return walkableTileMap;
    }
}
