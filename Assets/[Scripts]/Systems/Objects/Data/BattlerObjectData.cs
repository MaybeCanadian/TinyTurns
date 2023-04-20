using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Battler Object", menuName = "Objects/Battler Object")]
public class BattlerObjectData : PathfindingObjectData
{
    [Min(1)]
    public int maxHealth = 10;
}
