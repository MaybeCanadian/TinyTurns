using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlerObject : PathfindingObject
{
    public int maxHealth;
    public int currentHealth;

    public BattlerObject(BattlerObjectData data) : base(data)
    {

    }

    #region Health

    public void TakeDamage(int amount)
    {

    }

    #endregion
}
