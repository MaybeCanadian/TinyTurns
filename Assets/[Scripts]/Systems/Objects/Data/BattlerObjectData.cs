using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Battler Object", menuName = "Objects/Battler Object")]
public class BattlerObjectData : PathfindingObjectData
{
    [Min(0.001f)]
    public float maxHealth = 10;
    [Min(0.001f)]
    public float startingHealth;
}
