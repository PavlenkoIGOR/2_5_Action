using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu(fileName = "Create new Inventory Object", menuName = "MyInventorySystem/Create new Inventory Object")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> slots = new List<InventorySlot>();

    public void AddItem(ItemData i_data, int amount)
    {
        #region
        bool hasItem = false;
        for (int Items = 0; Items < slots.Count; Items++)
        {
            if (slots[Items].itemData == i_data && slots[Items].itemData.stackableType == StackableType.Stackable)
            {
                slots[Items].AddAmount(amount);
                hasItem = true;
                Debug.Log($"{slots[Items].itemData.itemName} q = {slots[Items].quantity} шт");
                break;
            }
            if (slots[Items].itemData == i_data && slots[Items].itemData.stackableType == StackableType.Unstackable)
            {
                slots.Add(new InventorySlot(i_data, 1));
                hasItem = true;                
                break;
            }
        }
        if (!hasItem)
        {
            slots.Add(new InventorySlot(i_data, amount));
        }
        #endregion

        #region with stackable obj
        #endregion
    }
}


[System.Serializable]
public class InventorySlot //описывает содержимое одной €чейки инвентар€
{
    public ItemData itemData;
    public int quantity;
    public InventorySlot(ItemData I_D, int q)
    {
        itemData = I_D;
        quantity = q;
    }
    public void AddAmount(int amount)
    {
        quantity += amount;
    }
}

