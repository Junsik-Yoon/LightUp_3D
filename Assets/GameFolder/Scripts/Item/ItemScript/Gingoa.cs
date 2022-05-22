using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gingoa : Equipable , Iinteractable
{
    public EquipItemData itemData;

    private void Awake()
    {
        description = itemData.description;
        itemName = itemData.name;
    }
    public override void Equip()
    {

    }

    
    public void Interact()
    {

        if(Input.GetButtonDown("Interact"))
        {
           
            //버튼을 눌렀을 시
            //돈이 있는지 체크하고 돈이 있으면 있는만큼 차감한 후 제거하고 
            //인벤토리 꽉 찼는지 체크하고 인벤토리로 들어가게
            InventoryManager.instance.FieldDescriptionToggle(false);
            //필드에서 remove하기
            InventoryManager.instance.InventoryAdd(itemData);
            Destroy(gameObject);
        }
        else
        {
            //레이캐스트로 체크만 될 시
            //설명창 띄우기
          //  InventoryManager.instance?.interactable;
            InventoryManager.instance.FieldDescriptionToggle(true,transform.position);
        }
    }

    public void InteractFinished()
    {
        InventoryManager.instance.FieldDescriptionToggle(false);
    }
}
