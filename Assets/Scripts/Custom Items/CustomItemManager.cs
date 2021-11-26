using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomItemManager : MonoBehaviour
{
    List<CustomItem> LoadedItems = new List<CustomItem>();
    List<CustomItem> DefaultItemTable = new List<CustomItem>();
    List<CustomItem> AllItemsTable = new List<CustomItem>();
    // Start is called before the first frame update


    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {

    }

    CustomItem SelectRandomItemFromTable(List<CustomItem> ItemTable)
    {
        List<CustomItem> DynamicTable = new List<CustomItem>(ItemTable);
        int randID = -1;
        int TableCount = ItemTable.Count;
        do
        {
            if (randID != -1)
            {
                DynamicTable.RemoveAt(randID);
            }
            randID = Random.Range(0, DynamicTable.Count);
        }
        while (LoadedItems.Contains(DynamicTable[randID]) && DynamicTable.Count != 0);

        if (DynamicTable[randID] !=null)
        {
            return DynamicTable[randID];
        }
        else
        {
            return null;
        }
    }

    void EquipCustomItem(CustomItem ci)
    {
        if(ci == null) return;
        if (LoadedItems.Contains(ci)) return;
        LoadedItems.Add(ci);
        ci.enabled = true;

    }
    void UnEquipCustomItem(CustomItem ci)
    {
        if (!LoadedItems.Contains(ci)) return;
        LoadedItems.Remove(ci);
        ci.enabled = false;
    }
}
