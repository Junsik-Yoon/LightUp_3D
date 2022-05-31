using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaHat :InteractableItem
{
    private void Awake()
    {
        itemPrice  = itemData.itemPrice;
        description = itemData.description;
        itemName = itemData.name;
    }
    public override void Equip()
    {
        MoneyManager.instance.santaDropStart=true;
        MoneyManager.instance.StartCoroutine(MoneyManager.instance.SantaDrop());
    }
    public override void UnEquip()
    {
        MoneyManager.instance.santaDropStart=false;        
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