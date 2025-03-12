using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "My New Aid item", menuName = "MyInventorySystem/Items/Aid")]

public class AidItem : ItemData
{
    public float healthRestore;
    void Awake()
    {
        type = ItemType.Aid;
    }
}
