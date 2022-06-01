using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFilledHelmet : InteractableItem
{
    private void Awake()
    {
        itemPrice  = itemData.itemPrice;
        description = itemData.description;
        itemName = itemData.name;
    }
    public override void Equip()
    {
        Player player = (GameObject.FindGameObjectWithTag("Player"))?.GetComponent<Player>();
        player.defaultDamage = player.damage;
        player.damage *=1.3f;
        player.playerLight.lightRunoutSpeed = 1.5f;
        
        Transform headPos = GameObject.Find("HeadParticle")?.transform;
        GameObject obj = Instantiate(Resources.Load<GameObject>("prefabs/EyeParticle"), headPos);
        BattleStageManager.instance.bloodFilledParticle = obj;
    }
    public override void UnEquip()
    {
        Player player = (GameObject.FindGameObjectWithTag("Player"))?.GetComponent<Player>();
        player.damage = player.defaultDamage;
        player.playerLight.lightRunoutSpeed = 1f;

        GameObject obj = BattleStageManager.instance.bloodFilledParticle;
        Destroy(obj);
        
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
