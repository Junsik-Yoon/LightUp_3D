using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInBattle : MonoBehaviour, Iinteractable
{
    public ItemData[] randomItems;
    public Transform[] genTransform;
    public void Interact()
    {

    }
    public void InteractFinished()
    {

    }
    public void GenerateItems()
    {
        int[] nums = new int[3];
        nums[0] = Random.Range(0,randomItems.Length);
        while(true)
        {
            nums[1] = Random.Range(0,randomItems.Length);
            if(nums[0]!=nums[1]) break;
        }
        while(true)
        {
            nums[2] = Random.Range(0,randomItems.Length);
            if(nums[0] != nums[2] && nums[1] != nums[2]) break;
        }
        for(int i=0; i<3; ++i)
        {
            GameObject obj = Instantiate(randomItems[nums[i]].prefab,genTransform[i].position,Quaternion.identity);
            (obj.GetComponent<InteractableItem>()).itemPrice = Random.Range(50,101);
//            Debug.Log(obj.GetComponent<InteractableItem>().itemPrice);
            obj.GetComponent<InteractableItem>().SetPrice();
        }
       
    }
}
