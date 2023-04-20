using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DamageStruct
{
    [Header("Damage")]
    public float amount;
    public DamageTypes type;

    [Header("Owner")]
    public BattlerObject source;

}

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
