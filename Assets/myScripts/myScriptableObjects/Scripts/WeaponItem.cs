using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "My New weapon item", menuName = "MyInventorySystem/Items/Weapon")]
public class WeaponItem : ItemData
{
    // Start is called before the first frame update
    void Awake()
    {
        type = ItemType.Weapon;
    }
}
