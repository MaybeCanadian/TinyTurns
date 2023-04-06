using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputsManager : MonoBehaviour
{
    public static PlayerInputsManager instance;

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
            Init();
        }
    }
    private void Init()
    {

    }
    #endregion
}
