using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log("Added " + item.itemName + " to inventory.");
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        Debug.Log("Removed " + item.itemName + " from inventory.");
    }

    public List<Item> GetItems()
    {
        return items;
    }
}