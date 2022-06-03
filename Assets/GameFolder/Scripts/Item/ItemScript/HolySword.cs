using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolySword : InteractableItem
{
    public GameObject lightSphere;
  //  private bool isEquipped=false;
    private void Awake()
    {
        itemPrice  = itemData.itemPrice;
        description = itemData.description;
        itemName = itemData.name;
    }

    public override void Equip()
    {
        BattleStageManager.instance.OnEnemyDead+=HolyPower;
    //    isEquipped=true;
    }
    public void HolyPower()
    {
        Vector3 pos = BattleStageManager.instance.cashingDeadEnemyPos;
        Vector3 fixedPos = new Vector3(pos.x,1f,pos.z);
        Instantiate(lightSphere,fixedPos,Quaternion.identity);
    }
    public override void UnEquip()
    {
        BattleStageManager.instance.OnEnemyDead-=HolyPower;       
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
