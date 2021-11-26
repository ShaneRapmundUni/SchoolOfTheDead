using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Individual Stats for the player, e.g. Health, Damage
/// </summary>
/// 
[Serializable]
public class PlayerStat
{
    public string StatName;
    public float BaseValue;
    public float Value
    {
        get
        {
            if (isDirty)
            {
                lastValue = CalculateFinalValue();
                isDirty = false;
            }
            return lastValue;
        }
    }
    
    private bool isDirty = true;
    private float lastValue;

    public List<StatModifier> statModifiers;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="baseValue">The default value of the stat at start</param>
    public PlayerStat(float baseValue, string statName)
    {
        StatName = statName;
        BaseValue = baseValue;
        statModifiers = new List<StatModifier>();
        isDirty = true;
    }

    /// <summary>
    /// Add a modifier to change this stat
    /// </summary>
    /// <param name="mod"></param>
    public void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }

    /// <summary>
    /// Used to sort the modifiers in the list
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
        {
            return -1;
        }
        else if (a.Order > b.Order)
        {    
            return 1;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Attempt to remove a stat modifier from this stat
    /// </summary>
    /// <param name="mod"></param>
    public bool RemoveModifier(StatModifier mod)
    {
        isDirty = true;
        return statModifiers.Remove(mod);
    }

    public bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;
        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    /// <summary>
    /// Calculate the value of the stat after all modifiers are applied
    /// </summary>
    /// <returns></returns>
    private float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentageAdditive = 0;
        if (statModifiers == null)
        {
            return finalValue;
        }
        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];
            if (mod.Type == StatModifierType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModifierType.PercentageAdditive)
            {
                sumPercentageAdditive += mod.Value;
                if (i + 1 >= statModifiers.Count || statModifiers[i+1].Type != StatModifierType.PercentageAdditive)
                {
                    finalValue *= 1 + sumPercentageAdditive;
                    sumPercentageAdditive = 0;
                }
            }
            else if (mod.Type == StatModifierType.PercentageMultiplicative)
            {
                finalValue *= 1 + mod.Value;
            }
        }
        return (float)Math.Round(finalValue, 2); //Floating point rounding compensation
    }
}

