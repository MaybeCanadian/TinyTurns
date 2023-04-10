using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerControllerManager
{
    public static List<PlayerController> players = null;

    public static void OutSideInit()
    {
        CheckInit();
    }
    private static void CheckInit()
    {
        if(players == null)
        {
            Init();
        }
    }
    private static void Init()
    {
        players = new List<PlayerController>();
    }
}
