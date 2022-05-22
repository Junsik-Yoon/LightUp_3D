using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUnit : MonoBehaviour
{  
    public Button button;
    public Image icon;


    ItemData curItemData;
    public void AddItem(ItemData itemData)
    {
        curItemData = itemData;

        icon.sprite = itemData.icon;
        icon.enabled = true;
        button.interactable = true;
    }
    public void RemoveItem()
    {
        
        curItemData = null;

        icon.sprite = null;
        icon.enabled = false;
        button.interactable = false;
    }
    public void UseItem()
    {
        Debug.Log(curItemData.name + "가 사용되었습니다");
        curItemData.Use();
    }

    public void MouseEnter()
    {
        if(InventoryManager.instance.isInvenReacting) return;
        // 아이템에 대한 설명에 대한 ui 생성

        Text[] text = InventoryManager.instance.invenDescription.GetComponentsInChildren<Text>();

        if(curItemData!=null)
        {
            for(int i=0; i<text.Length;++i)
            {
                if(text[i].gameObject.name == "ItemName")
                {
                    text[i].text = curItemData.name;        
                }
                else if (text[i].gameObject.name == "ItemDescription")
                {
                    text[i].text = curItemData.description;
                }
            }
            Vector3 calculatedPos;
            if(transform.position.x<300f)
            {
                calculatedPos = new Vector3(transform.position.x+100f,transform.position.y,transform.position.z+50f);
            }
            else
            {
                calculatedPos = new Vector3(transform.position.x-100f,transform.position.y,transform.position.z+50f);
            }
            
            
            InventoryManager.instance.invenDescription.transform.position = calculatedPos;
            InventoryManager.instance.invenDescription.SetActive(true);   
        }

    }
    public void MouseExit()
    {
        InventoryManager.instance.invenDescription.SetActive(false);  
    }
    public void ButtonDown()
    {
        // 아이템설명 ui 지우기
        // 아이템이 있다면 마우스 위치에 equip 혹은 remove 혹은 cancel ui 생성 
       // Debug.Log(transform.position);
        Vector3 calculatedPos;
        if(transform.position.x<300f)
        {
            calculatedPos = new Vector3(transform.position.x+80f,transform.position.y,transform.position.z+50f);
        }
        else
        {
            calculatedPos = new Vector3(transform.position.x-80f,transform.position.y,transform.position.z+50f);
        }
        
            
        InventoryManager.instance.invenInteract.transform.position = calculatedPos;
        InventoryManager.instance.equipInteract.SetActive(false);
        InventoryManager.instance.invenInteract.SetActive(true);
        InventoryManager.instance.invenDescription.SetActive(false);  
        InventoryManager.instance.currentlySelectedItem = curItemData;
        InventoryManager.instance.isInvenReacting=true;
        
    }
    public void OnEquipButton()
    {
        InventoryManager.instance.InventoryRemove(curItemData);
        InventoryManager.instance.EquipAdd(curItemData);
    }
    public void OnRemoveButton()
    {
        InventoryManager.instance.InventoryRemove(curItemData);
    }
}