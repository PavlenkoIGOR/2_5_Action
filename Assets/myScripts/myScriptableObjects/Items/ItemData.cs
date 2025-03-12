using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Aid,
    Default
}public enum StackableType
{
    Stackable,
    Unstackable
}

public abstract class ItemData : ScriptableObject
{
    public string itemID;
    public string itemName;
    public Sprite icon;
    public GameObject prefab;
    public ItemType type;
    public StackableType stackableType;


    [TextArea]
    public string description;
}
