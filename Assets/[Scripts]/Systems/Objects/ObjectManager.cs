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
    public static void CreateObject(ObjectData data, Vector2Int startPos)
    {
        CheckInit();

        if(data == null)
        {
            Debug.LogError("ERROR - Could not create object as object data is null.");
            return;
        }

        Object obj = new (data);

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

        Debug.Log("Connected Object to Parent");
    }
    #endregion
}
