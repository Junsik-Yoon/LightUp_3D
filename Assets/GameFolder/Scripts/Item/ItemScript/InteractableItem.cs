using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableItem  : Equipable, Iinteractable
{
    public int itemPrice;
    public EquipItemData itemData;
    Text textPrice;
    private void Awake()
    {
        itemPrice  = itemData.itemPrice;
        description = itemData.description;
        itemName = itemData.name;
    }
    private void Start()
    {
        if(itemPrice==0) return;
        textPrice = SetPrice();
        
    }
    public Text SetPrice()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("prefabs/MoneyCanvas"),transform.position,Quaternion.identity);
        obj.transform.parent = transform;
        Vector3 textPos = new Vector3(transform.position.x,transform.position.y-0.9f,transform.position.z-0.3f);
       // Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        (obj.transform.GetChild(0)).transform.position = textPos;
        (obj.GetComponentInChildren<Text>()).text = itemPrice.ToString();
        return obj.GetComponentInChildren<Text>();
    }
    public void ChangePrice(int itemPrice)
    {
        this.itemPrice = itemPrice;
        if(textPrice!=null)
        {
            textPrice.text = itemPrice.ToString();
        }

    }
    public virtual void Interact()
    {

        if(Input.GetButtonDown("Interact"))
        {
            if(itemPrice>0)
            {
                if(MoneyManager.instance.money - itemPrice>=0)
                {
                    MoneyManager.instance.money-=itemPrice;
                    InventoryManager.instance.FieldDescriptionToggle(false);
                    //필드에서 remove하기
                    InventoryManager.instance.InventoryAdd(itemData);
                    Destroy(gameObject);
                }
                else
                {
                    //가진 돈이 부족하다는 메세지 띄우기
                    //혹은 사운드
                    InventoryManager.instance.OutOfMoney();
                }
            }
            else
            {
                    InventoryManager.instance.FieldDescriptionToggle(false);
                    //필드에서 remove하기
                    InventoryManager.instance.InventoryAdd(itemData);
                    Destroy(gameObject);
            }
            //버튼을 눌렀을 시
            //돈이 있는지 체크하고 돈이 있으면 있는만큼 차감한 후 제거하고 
            //인벤토리 꽉 찼는지 체크하고 인벤토리로 들어가게

        }
        else
        {
            //레이캐스트로 체크만 될 시
            //설명창 띄우기
          //  InventoryManager.instance?.interactable;
            InventoryManager.instance.FieldDescriptionToggle(true,transform.position);
        }
    }

    public virtual void InteractFinished()
    {
        InventoryManager.instance.FieldDescriptionToggle(false);
    }

    

}
