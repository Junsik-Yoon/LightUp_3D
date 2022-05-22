using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipUI : MonoBehaviour
{
    EquipUnit[] equipUnits;

    public void UpdateUI()
    {
        
        equipUnits = GetComponentsInChildren<EquipUnit>();
        for(int i=0; i< equipUnits.Length;++i)
        {
            if(i<InventoryManager.instance.equipItems.Count)
            {
                equipUnits[i].AddItem(InventoryManager.instance.equipItems[i]);
            }
            else
            {
                equipUnits[i].RemoveItem();
            }
        }
    }
}
