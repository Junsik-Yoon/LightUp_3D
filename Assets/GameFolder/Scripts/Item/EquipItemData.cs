using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EquipItemData",menuName ="Data/EquipItem")]
public class EquipItemData : ItemData
{
    public Equipable equipable;
    // public enum gearType
    // {
    //     OneHandWeapon,
    //     TwoHandWeapon,
    //     Helmet,
    //     Armour,
    //     Necklace,
    //     Ring,
    //     Earing,
    //     Shield,
    //     Belt,
    //     Boots,
    //     Gloves,
    //     Default,
    
    // }
    // public gearType gear_type;
    public override void Use()
    {
        equipable.Equip();

        // EquipManager.instance.Add(this);
        
        // InventoryManager.instance.Remove(this);
        // Debug.Log(name+ "을 착용합니다");
        // //Instantiate(prefab,new Vector2(0,0),Quaternion.identity);
        
        // PlayerMover pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMover>();
        // pm.isWielded=true;
        // pm._curEquipItem = this;
        // EquipManager.instance.Add(this);
        
        

        // InventoryManager.instance.Remove(this);
        
    }
}
