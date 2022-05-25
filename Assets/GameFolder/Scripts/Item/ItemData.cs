using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public GameObject prefab = null;
    [TextArea]
    public string description;
    public int itemPrice;
    public abstract void Use();

    public void Remove()
    {
        InventoryManager.instance.InventoryRemove(this);
    }
}
