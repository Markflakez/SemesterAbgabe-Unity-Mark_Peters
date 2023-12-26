using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryBehaviour : MonoBehaviour
{
    public Item witchHat;
    public Item key;
    public Item necklace;
    public InventoryManager inventoryManager;

    //Platzhalter für die Inventar-Testfunktionen
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryManager.AddItemToInventory(witchHat);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            inventoryManager.AddItemToInventory(key);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            inventoryManager.AddItemToInventory(necklace);
        }
    }
}
