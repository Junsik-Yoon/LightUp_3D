using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    InventoryUnit[] inventoryUnits;

    public void UpdateUI()
    {
        inventoryUnits = GetComponentsInChildren<InventoryUnit>();
        for(int i=0; i< inventoryUnits.Length;++i)
        {
            if(i<InventoryManager.instance.inventoryItems.Count)
            {
                inventoryUnits[i].AddItem(InventoryManager.instance.inventoryItems[i]);
            }
            else
            {
                inventoryUnits[i].RemoveItem();
            }
        }
    }

}
