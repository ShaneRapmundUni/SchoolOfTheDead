using System.Collections.Generic;
using UnityEngine;

[UnityEngine.CreateAssetMenu(menuName = "Player Item")]
[System.Serializable]
public class PlayerItem : UnityEngine.ScriptableObject
{
    public List<Item> modifiers;
    public void Equip()
    {
        for (int i = 0; i < modifiers.Count; i++)
        {
            PlayerStats.Instance.stats[(int)modifiers[i].ItemStat].AddModifier(modifiers[i].modifier); //replace with scriptableobjects
        }
    }

    public void UnEquip()
    {
        for (int i = 0; i < modifiers.Count; i++)
        {
            //player.stats[modifiers.itemstat].RemoveAllModifiersFromSource(this);
            PlayerStats.Instance.stats[(int)modifiers[i].ItemStat].RemoveAllModifiersFromSource(this); //replace with scriptableobjects , //currently source might not me set in the inspector properly
        }
    }

    //Update value in editor when a value is changed
    private void OnValidate()
    {
        for (int i = 0; i < modifiers.Count; i++)
        {
            modifiers[i].modifier.UpdateOrder();
        }
    }
}
