using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIDataBase
{
    public static Dictionary<UIList, GameObject> UIDict = null;

    #region Init Functions
    public static void OutSideInit()
    {
        CheckInit();
    }
    private static void CheckInit()
    {
        if(UIDict == null)
        {
            Init();
        }
    }
    private static void Init()
    {
        SetUpDictionary();

        Debug.Log("UI Data Base Init");
    }
    private static void SetUpDictionary()
    {
        foreach(int uiName in Enum.GetValues(typeof(UIList)))
        {
            GameObject UI = Resources.Load<GameObject>("UI/" + (UIList)uiName);

            if(UI == null)
            {
                continue;
            }

            UIDict.Add((UIList)uiName, UI);
            continue;
        }
    }
    #endregion

    #region UI Get
    public static GameObject GetUI(UIList ui)
    {
        CheckInit();

        if(!UIDict.ContainsKey(ui))
        {
            Debug.LogError("ERROR - UI Dict does not have an entry for the given key.");
            return null;
        }

        return UIDict[ui];
    }
    public static bool CheckHasUI(UIList ui)
    {
        CheckInit();

        return UIDict.ContainsKey(ui);
    }
    #endregion
}
