using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct ObjectBlockingStruct
{
    [Header("Non-Solid will not block pathing")]
    public bool solid;

    [Header("Faction based pathing")]
    public Factions faction;
    public bool allowAnyPathingThrough;
    public bool allowFactionPathingThrough;

    public bool allowAnyStandOn;
    public bool allowFactionStandOn;
}