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
    public TargetTypes targetType;

    [Header("Owner")]
    public BattlerObject source;
}

[System.Serializable]
public struct ResistanceStruct
{
    [Header("Resistance")]
    public float amount;
    public DamageTypes damageType;
    public TargetTypes targetType;
    public ModificationTypes modificationType;
}

[System.Serializable]
public struct HealingStruct
{
    public float amount;
    public HealingSources healingSource;
    public TargetTypes targetType;
}
public struct HealingBuffStruct
{
    public float amount;
    public HealingSources healingSource;
    public TargetTypes targetType;
    public ModificationTypes modificationType;

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
public enum ModificationTypes
{
    PERCENT,
    FLAT
}

[System.Serializable]
public enum HealingSources
{
    UNTYPED,
    MAGICAL,
    POTION,
    RESTING
}
#endregion


