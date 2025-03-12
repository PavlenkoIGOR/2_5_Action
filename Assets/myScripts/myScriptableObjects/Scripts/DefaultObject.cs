using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "My New default item", menuName = "MyInventorySystem/Items/Default")]
public class DefaultObject : ItemData
{
    private void Awake()
    {
        type = ItemType.Default;
    }
}
