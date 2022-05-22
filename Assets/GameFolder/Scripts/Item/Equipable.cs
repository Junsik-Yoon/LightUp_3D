using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipable : MonoBehaviour
{
    public string description;
    public string itemName;
    public abstract void Equip();
}
