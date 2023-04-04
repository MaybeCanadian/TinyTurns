using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityModelDataBase
{
    private static Dictionary<EntityList, GameObject> entityModelDict = null;

    #region Init Functions
    public static void OutSideInit()
    {
        CheckInit();
    }
    private static void CheckInit()
    {
        if(entityModelDict == null)
        {
            Init();
        }
    }
    private static void Init()
    {
        LoadEnitities();
    }
    private static void LoadEnitities()
    {
        entityModelDict = new Dictionary<EntityList, GameObject>();

        foreach(int entityName in Enum.GetValues(typeof(EntityList)))
        {
            if((EntityList)entityName == EntityList.NULL)
            {
                continue;
            }

            GameObject entity = Resources.Load<GameObject>("Entities/" + ((EntityList)entityName).ToString());

            if(entity == null)
            {
                Debug.Log("Could not load model for " + (EntityList)entityName);
                continue;
            }

            entityModelDict.Add((EntityList)entityName, entity);
        } 
    }
    #endregion

    #region Model
    public static GameObject GetModel(EntityList entity)
    {
        CheckInit();

        if(entity == EntityList.NULL) 
        {
            Debug.LogError("ERROR - Null is not a valid entity name, assignment missing.");
            return null;
        }

        if(!entityModelDict.ContainsKey(entity))
        {
            Debug.LogError("ERROR - Enitity Dictionary does not have a model for the given key.");
            return null;
        }

        return entityModelDict[entity];
    }
    public static bool CheckHasModel(EntityList entity)
    {
        CheckInit();

        return entityModelDict.ContainsKey(entity);
    }
    #endregion
}
