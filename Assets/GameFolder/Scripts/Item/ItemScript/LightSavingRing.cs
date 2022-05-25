using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSavingRing : InteractableItem
{
    private void Awake()
    {
        itemPrice  = itemData.itemPrice;
        description = itemData.description;
        itemName = itemData.name;
    }
    public override void Equip()
    {
         
    }
    public override void UnEquip()
    {

    }
    public override void Interact()
    {
        base.Interact();
    }
    public override void InteractFinished()
    {
        base.InteractFinished();
    }
}