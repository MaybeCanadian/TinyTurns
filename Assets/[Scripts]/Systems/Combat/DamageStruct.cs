using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Structs
[System.Serializable]
public struct DamageStruct
{
    [Header("Damage")]
    public float amount;
    public DamageTypes damageType;

    [Header("Owner")]
    public BattlerObject source;
}

[System.Serializable]
public struct ResistanceStruct
{
    [Header("Resistance")]
    public float amount;
    public DamageTypes damageType;
    public ResistanceTypes resistanceType;
}

[System.Serializable]
public struct HealingStruct
{
    public float amount;
}
#endregion


#region Enums
[System.Serializable]
public enum DamageTypes
{
    UNTYPED,
    PHYSICAL,
    MAGICAL
}

[System.Serializable]
public enum TargetTypes
{
    UNTYPED,
    SINGLE,
    AOE,
    DOT,
    AURA
}

[System.Serializable]
public enum ResistanceTypes
{
    PERCENT,
    FLAT
}
#endregion


