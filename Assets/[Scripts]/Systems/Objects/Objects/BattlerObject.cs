using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlerObject : PathfindingObject
{
    public float maxHealth = 10.0f;
    public float currentHealth;

    public BattlerObject(BattlerObjectData data) : base(data)
    {
        if(data != null)
        {
            maxHealth = data.maxHealth;
            currentHealth = maxHealth;
        }
    }

    #region Health

    public void TakeDamage(DamageStruct damage)
    {
        
    }
    public void Heal(HealingStruct healing)
    {

    }

    #endregion
}
