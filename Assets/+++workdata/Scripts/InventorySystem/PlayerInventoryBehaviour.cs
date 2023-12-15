using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryBehaviour : MonoBehaviour
{
    public Item apple;
    public InventoryManager inventoryManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryManager.AddItemToInventory(apple);
        }
    }
}
