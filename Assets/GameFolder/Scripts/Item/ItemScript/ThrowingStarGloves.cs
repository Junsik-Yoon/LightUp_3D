using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingStarGloves : InteractableItem
{
    private void Awake()
    {
        itemPrice  = itemData.itemPrice;
        description = itemData.description;
        itemName = itemData.name;
    }
    public override void Equip()
    {
         ItemEffectOnPlayerManager.instance.isEquipShuriken=true;
    }
    public override void UnEquip()
    {
        ItemEffectOnPlayerManager.instance.isEquipShuriken=false;
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