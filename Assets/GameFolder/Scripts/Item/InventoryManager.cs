
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance {get;private set;}
    public List<ItemData> inventoryItems ;
    public List<ItemData> equipItems;
    public InventoryUI ui;
    public EquipUI equipUI;
    public GameObject inventoryOnOff;
    public GameObject fieldDescription;
    public GameObject invenDescription;
    public GameObject invenInteract;
    public GameObject equipDescription;
    public GameObject equipInteract;
    Collider savedCollider;
    public int inventoryMaxSize = 12;
    public int equipMaxSize = 4;
    public bool isInvenReacting=false;
    public ItemData currentlySelectedItem;
    private void Awake()
    {
        instance = this;
        inventoryItems = new List<ItemData>();
    }
    private void Update()
    {
        InventoryListener();
    }
    public void InventoryListener()
    {
        if(!Input.GetButtonDown("InventoryInteract")) return;

        inventoryOnOff.gameObject.SetActive(!inventoryOnOff.gameObject.activeSelf);
        ui.UpdateUI();
        equipUI.UpdateUI();
    }

    public void FieldDescriptionToggle(bool status) //status가 false일시에 쓰도록
    {
       // fieldDescription.SetActive(!fieldDescription.activeSelf);
     //   interactable.
        fieldDescription.SetActive(status);
    }
    public void FieldDescriptionToggle(bool status,Vector3 objPos)
    {
       // fieldDescription.SetActive(!fieldDescription.activeSelf);
        Text[] text = fieldDescription.GetComponentsInChildren<Text>();
        for(int i=0; i<text.Length;++i)
        {
            if(text[i].gameObject.name == "ItemName")
            {
                if(savedCollider.gameObject.GetComponent<Equipable>())
                {
                    text[i].text = savedCollider.gameObject.GetComponent<Equipable>().itemName;
                }
                
            }
            else if (text[i].gameObject.name == "ItemDescription")
            {
                if(savedCollider.gameObject.GetComponent<Equipable>())
                {
                    text[i].text = savedCollider.gameObject.GetComponent<Equipable>().description;
                }
            }
        }
        fieldDescription.SetActive(status);
        if(status)
        {
            Vector3 calculatedPos = GameObject.Find("Player Camera").GetComponent<Camera>().WorldToScreenPoint(new Vector3(objPos.x-3f,objPos.y+1f,objPos.z-3f));
            //fieldDescription.transform.position = new Vector3(objPos.x-10f,objPos.y+1f,objPos.z-10f);
            fieldDescription.transform.position = calculatedPos;
        }

    }
    public void SetInformation(Collider coll)
    {
        savedCollider = coll;
    }

    public bool InventoryAdd(ItemData item)
    {
        if(inventoryItems.Count >=inventoryMaxSize)
        {
            return false;
        }
        inventoryItems.Add(item);
        ui.UpdateUI();
        equipUI.UpdateUI();
        return true;
    }
    public void InventoryRemove(ItemData item)
    {
        inventoryItems.Remove(item);
        ui.UpdateUI();
        equipUI.UpdateUI();
    }
    public bool EquipAdd(ItemData item)
    {
        if(equipItems.Count >=equipMaxSize)
        {
            return false;
        }
        equipItems.Add(item);
        ui.UpdateUI();
        equipUI.UpdateUI();
        return true;
        // 능력 되도록 equipItems[0].Use();
    }
    public void EquipRemove(ItemData item)
    {
        equipItems.Remove(item);
        ui.UpdateUI();
        equipUI.UpdateUI();
    }

    //inventory ui button interact
    public void OnCloseButton()
    {
        inventoryOnOff.SetActive(false);
    }
    public void OnInventoryOpen()
    {
        inventoryOnOff.SetActive(true);
        ui.UpdateUI();
        equipUI.UpdateUI();
    }
    public void OnEquipButton()
    {
        if(equipItems.Count>=equipMaxSize) return;
        EquipAdd(currentlySelectedItem);
        InventoryRemove(currentlySelectedItem);
        invenInteract.SetActive(false);
        invenDescription.SetActive(false);
        isInvenReacting=false;  
    }
    public void OnRemoveButton()
    {
         InventoryRemove(currentlySelectedItem);
        invenInteract.SetActive(false);
        invenDescription.SetActive(false);
        isInvenReacting=false;  
    }
    public void OnCancelButton()
    {
        invenInteract.SetActive(false);
        invenDescription.SetActive(false);
        isInvenReacting=false;  
    }
    public void OnUnequipButton()
    {
        if(inventoryItems.Count>=inventoryMaxSize) return;
        InventoryAdd(currentlySelectedItem);
        EquipRemove(currentlySelectedItem);
        equipInteract.SetActive(false);
        equipDescription.SetActive(false);
        isInvenReacting=false;  
    }
    public void OnEquipCancelButton()
    {
        equipInteract.SetActive(false);
        equipDescription.SetActive(false);
        isInvenReacting=false;  
    }
}

