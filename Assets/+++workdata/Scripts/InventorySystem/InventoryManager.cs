using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> inventorySlots;

    public void AddItemToInventory(Item newItem)
    {
        InventorySlot emptySlot = GetEmptySlot();
        if (emptySlot != null)
        {
            emptySlot.AddItem(newItem);
        }
    }

    private InventorySlot GetEmptySlot()
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.scriptableItem == null)
            {
                return slot;
            }
        }
        return null;
    }
}
