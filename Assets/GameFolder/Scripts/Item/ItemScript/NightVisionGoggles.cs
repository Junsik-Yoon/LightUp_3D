using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightVisionGoggles : InteractableItem
{
    private void Awake()
    {
        itemPrice  = itemData.itemPrice;
        description = itemData.description;
        itemName = itemData.name;
    }
    public override void Equip()
    {
        
        (Camera.main.GetComponent<BeautifyEffect.Beautify>()).nightVision = true;  
    }
    public override void UnEquip()
    {
        (Camera.main.GetComponent<BeautifyEffect.Beautify>()).nightVision = false;
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
