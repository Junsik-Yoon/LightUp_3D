using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessOfLight : InteractableItem
{
    public GameObject lightShieldEffect;

    GameObject cashingEquipObj;
    private void Awake()
    {
        itemPrice  = itemData.itemPrice;
        description = itemData.description;
        itemName = itemData.name;
    }

    public override void Equip()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Transform playerRoot = (player.gameObject.transform.GetChild(1));
        cashingEquipObj = Instantiate(lightShieldEffect,playerRoot.position,Quaternion.Euler(-90f,0,0));
        cashingEquipObj.transform.SetParent(playerRoot);
    }
    public override void UnEquip()
    {
        Destroy(cashingEquipObj);
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
