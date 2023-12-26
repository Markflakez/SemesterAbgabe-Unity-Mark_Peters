using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> inventorySlots;

    #region Kommentare
    //F�gt ein neues Item zum Inventar hinzu
    //Wenn ein passender Slot vorhanden ist, wird die Anzahl erh�ht
    //Andernfalls wird versucht, einen leeren Slot zu finden oder einen mit dem gleichen Item
    #endregion
    public void AddItemToInventory(Item newItem)
    {
        InventorySlot existingSlot = GetExistingSlot(newItem);

        if (existingSlot != null)
        {
            existingSlot.IncreaseItemCount(newItem.itemCount);
        }
        else
        {
            InventorySlot emptySlot = GetEmptySlot();

            if (emptySlot != null)
            {
                Item clonedItem = Instantiate(newItem);
                emptySlot.AddItem(clonedItem);
            }
            else
            {
                emptySlot = GetSlotWithSameItem(newItem);

                if (emptySlot != null)
                {
                    emptySlot.IncreaseItemCount(newItem.itemCount);
                }
            }
        }
    }

    #region Kommentare
    //Sucht nach einem leeren Slot und gibt diesen zur�ck
    #endregion
    private InventorySlot GetEmptySlot()
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.item == null)
            {
                return slot;
            }
        }
        return null;
    }

    #region Kommentare
    //�berpr�ft, ob es einen Slot gibt, der das Item enth�lt, und gibt diesen zur�ck
    #endregion
    private InventorySlot GetExistingSlot(Item newItem)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.item != null && slot.item.itemName == newItem.itemName &&
                slot.item.itemCount < slot.item.maxStackCount)
            {
                return slot;
            }
        }
        return null;
    }

    #region Kommentare
    //Gibt den ersten Slot zur�ck, der das gleiche Item enth�lt
    #endregion
    private InventorySlot GetSlotWithSameItem(Item newItem)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.item != null && slot.item.itemName == newItem.itemName)
            {
                return slot;
            }
        }
        return null;
    }
}
