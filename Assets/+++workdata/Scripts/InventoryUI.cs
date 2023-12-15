using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI inventoryText;
    private Inventory inventory;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        string inventoryString = "Inventory:\n";

        foreach (Item item in inventory.GetItems())
        {
            inventoryString += "- " + item.itemName + "\n";
        }

        inventoryText.text = inventoryString;
    }
}