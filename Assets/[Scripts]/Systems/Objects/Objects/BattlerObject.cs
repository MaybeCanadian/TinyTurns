using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlerObject : PathfindingObject
{
    #region Event Dispatchers
    public delegate void HealthEvent(float amount);
    public HealthEvent OnHealthLostUnAdjusted;
    public HealthEvent OnHealthGainedUnadjusted;

    public HealthEvent OnHealthLostAdjusted;
    public HealthEvent OnHealthGainedAdjusted;

    public HealthEvent OnHealthPercentChanged;

    public delegate void BattlerEvent();
    public BattlerEvent OnBattlerDied;
    #endregion

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
        float amount = damage.amount;

        LinkedList<float> percentModifications = new LinkedList<float>();
        LinkedList<float> flatModifications = new LinkedList<float>();

        if (data != null)
        {
            foreach (ResistanceStruct resistance in (data as BattlerObjectData).resistances)
            {
                if(resistance.damageType == damage.damageType || resistance.damageType == DamageTypes.UNTYPED)
                {
                    if (resistance.targetType == damage.targetType || resistance.targetType == TargetTypes.UNTYPED)
                    {
                        if (resistance.modificationType == ModificationTypes.PERCENT)
                        {
                            percentModifications.AddLast(resistance.amount);
                            continue;
                        }

                        flatModifications.AddLast(resistance.amount);
                    }
                }
            }
        }

        foreach(float flat in flatModifications)
        {
            amount -= flat;
        }

        foreach(float percent in percentModifications)
        {
            amount /= percent;
        }

        amount = Mathf.Min(amount, 0.0f);

        float oldCurrent = currentHealth;

        currentHealth -= amount;

        currentHealth = Mathf.Min(currentHealth, 0.0f);

        OnHealthLostUnAdjusted?.Invoke(amount);
        OnHealthLostAdjusted?.Invoke(oldCurrent - currentHealth);

        CheckDead();
    }
    public void Heal(HealingStruct healing)
    {
        float amount = healing.amount;

        LinkedList<float> percentModifications = new LinkedList<float>();
        LinkedList<float> flatModification = new LinkedList<float>();

        if(data != null)
        {
            foreach(HealingBuffStruct buff in (data as BattlerObjectData).healingBuffs)
            {
                if(buff.healingSource == healing.healingSource || buff.healingSource == HealingSources.UNTYPED)
                {
                    if(buff.targetType == healing.targetType || buff.targetType == TargetTypes.UNTYPED)
                    {
                        if(buff.modificationType == ModificationTypes.PERCENT)
                        {
                            percentModifications.AddLast(buff.amount);
                            continue;
                        }

                        flatModification.AddLast(buff.amount);
                        continue;
                    }
                }
            }
        }

        foreach(float flat in flatModification)
        {
            amount += flat;
        }

        foreach(float percent in percentModifications)
        {
            amount *= percent;
        }

        float oldCurrent = currentHealth;

        currentHealth += amount;

        currentHealth = Mathf.Max(currentHealth, maxHealth);

        OnHealthGainedUnadjusted?.Invoke(amount);
        OnHealthGainedAdjusted?.Invoke(currentHealth - oldCurrent);

        CheckDead();
    }
    private void CheckDead()
    {
        if(currentHealth <= 0.0f)
        {
            PlayerDead();

            OnBattlerDied?.Invoke();
        }

        OnHealthPercentChanged?.Invoke(currentHealth / maxHealth);
    }
    private void PlayerDead()
    {
        //o sheet he dead
    }

    #endregion
}
