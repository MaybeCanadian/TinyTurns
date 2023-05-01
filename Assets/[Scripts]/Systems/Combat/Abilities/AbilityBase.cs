using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Base Ability", menuName = "Combat/Abilities/Ability Base")]
public class AbilityBase : ScriptableObject
{
    public string displayName = "Default Name";
    public Sprite icon = null;
}
