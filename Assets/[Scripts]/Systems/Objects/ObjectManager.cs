using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectManager
{
    #region Event Dispatchers
    public delegate void ObjectEvent(Object obj);
    public static ObjectEvent OnObjectCreated;
    #endregion

    public static List<Object> objects;
    public static GameObject ObjectParent = null;

    #region Init Functions
    public static void OutSideInit()
    {
        CheckInit();
    }
    private static void CheckInit()
    {
        if(objects == null)
        {
            Init();
        }
    }
    private static void Init()
    {
        objects = new List<Object>();

        SetUpObjectParent();
    }
    private static void SetUpObjectParent()
    {
        if(ObjectParent == null)
        {
            ObjectParent = new GameObject();
            ObjectParent.name = "[Objects]";
        }

        return;
    }
    #endregion

    #region Object Functions
    public static Object CreateObject(ObjectData data, Vector2Int startPos)
    {
        CheckInit();

        if(data == null)
        {
            Debug.LogError("ERROR - Could not create object as object data is null.");
            return null;
        }

        Object obj = new Object(data);

        SetUpObject(obj, startPos);

        return obj;
    }
    public static PathfindingObject CreatePathfindingObject(PathfindingObjectData data, Vector2Int startPos)
    {
        CheckInit();

        if (data == null)
        {
            Debug.LogError("ERROR - Could not create object as object data is null.");
            return null;
        }

        PathfindingObject obj = new PathfindingObject(data);

        SetUpObject(obj, startPos);

        return obj;
    }
    public static PlayerObject CreatePlayerObject(PlayerObjectData data, Vector2Int startPos)
    {
        CheckInit();

        if (data == null)
        {
            Debug.LogError("ERROR - Could not create object as object data is null.");
            return null;
        }

        PlayerObject obj = new PlayerObject(data);

        SetUpObject(obj, startPos);

        return obj;
    }
    private static void SetUpObject(Object obj, Vector2Int startPos)
    {
        obj.PlaceObjectAtGridPos(startPos);

        obj.CreateVisuals();

        objects.Add(obj);

        OnObjectCreated?.Invoke(obj);
    }
    public static void ConnectParent(GameObject obj)
    {
        if(ObjectParent == null)
        {
            SetUpObjectParent();
        }

        obj.transform.parent = ObjectParent.transform;

        //Debug.Log("Connected Object to Parent");
    }
    #endregion
}

public enum ObjectTypeFilters
{
    None,
    Object,
    Pathfinding,
    Player,
    UI
}
