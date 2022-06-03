using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPowerGloves  : InteractableItem
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
        player.playerLight.lightRunoutSpeed +=0.5f;
        
        player.hitImpactEffect = Resources.Load<GameObject>("prefabs/GlowEffect");
    }
    public override void UnEquip()
    {
        Player player = (GameObject.FindGameObjectWithTag("Player"))?.GetComponent<Player>();
        player.damage = player.defaultDamage;
        player.playerLight.lightRunoutSpeed -= 0.5f;
        
        player.hitImpactEffect = Resources.Load<GameObject>("prefabs/MeleeHitEffectClone");
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