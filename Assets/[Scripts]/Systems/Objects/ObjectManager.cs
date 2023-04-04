using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectManager
{
    public static List<Object> objects;

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
    }
    #endregion

}
