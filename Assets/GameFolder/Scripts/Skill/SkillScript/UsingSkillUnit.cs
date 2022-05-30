using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsingSkillUnit : MonoBehaviour
{
    public Button button;
    public Image icon;

    public SkillData curSkillData;
    public void AddItem(SkillData skillData)
    {
        curSkillData = skillData;

        icon.sprite = skillData.icon;
        icon.enabled = true;
        button.interactable = true;
    }
    public void RemoveItem()
    {
        
        curSkillData = null;

        icon.sprite = null;
        icon.enabled = false;
        button.interactable = false;
    }
    public void UseItem()
    {
        curSkillData.Use();
    }
    public void OnButtonDown()
    {
        SkillManager.instance.UsingSkillRemove(curSkillData);
    }

    // public void MouseEnter()
    // {
    //     if(InventoryManager.instance.isInvenReacting) return;
    //     // 아이템에 대한 설명에 대한 ui 생성

    //     Text[] text = InventoryManager.instance.equipDescription.GetComponentsInChildren<Text>();

    //     if(curSkillData!=null)
    //     {
    //         for(int i=0; i<text.Length;++i)
    //         {
    //             if(text[i].gameObject.name == "ItemName")
    //             {
    //                 text[i].text = curItemData.name;        
    //             }
    //             else if (text[i].gameObject.name == "ItemDescription")
    //             {
    //                 text[i].text = curItemData.description;
    //             }
    //         }
    //         Vector3 calculatedPos;
    //         if(transform.position.x<300f)
    //         {
    //             calculatedPos = new Vector3(transform.position.x+100f,transform.position.y,transform.position.z+50f);
    //         }
    //         else
    //         {
    //             calculatedPos = new Vector3(transform.position.x-100f,transform.position.y,transform.position.z+50f);
    //         }
            
            
    //         InventoryManager.instance.equipDescription.transform.position = calculatedPos;
    //         InventoryManager.instance.equipDescription.SetActive(true);   
    //     }

    // }
    // public void MouseExit()
    // {
    //     InventoryManager.instance.equipDescription.SetActive(false);  
    // }
    // public void ButtonDown()
    // {
    //     // 아이템설명 ui 지우기
    //     // 아이템이 있다면 마우스 위치에 equip 혹은 remove 혹은 cancel ui 생성 
    //    // Debug.Log(transform.position);
    //     Vector3 calculatedPos;
    //     if(transform.position.x<300f)
    //     {
    //         calculatedPos = new Vector3(transform.position.x+80f,transform.position.y,transform.position.z+50f);
    //     }
    //     else
    //     {
    //         calculatedPos = new Vector3(transform.position.x-80f,transform.position.y,transform.position.z+50f);
    //     }
        
            
    //     InventoryManager.instance.equipInteract.transform.position = calculatedPos;
    //     InventoryManager.instance.invenInteract.SetActive(false);
    //     InventoryManager.instance.equipInteract.SetActive(true);
    //     InventoryManager.instance.equipDescription.SetActive(false);  
    //     InventoryManager.instance.currentlySelectedItem = curItemData;
    //     InventoryManager.instance.isInvenReacting=true;
        
    // }

}
