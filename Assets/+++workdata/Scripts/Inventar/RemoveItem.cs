using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemoveItem : MonoBehaviour
{
    private InventorySlot inventorySlot;
    private InventoryManager inventoryManager;
    public AudioSource removeItemAudioSource;

    #region Awake Kommentare
    //Awake-Methode wird aufgerufen, um Referenzen zu initialisieren und die UI-Farbe festzulegen
    #endregion
    public void Awake()
    {
        inventorySlot = GetComponent<InventorySlot>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        SetInstructionColor("RemoveItemInstruction", Color.gray);
    }

    #region DecreaseItemCount Kommentare
    //Entfernt ein Item aus dem Inventar und aktualisiert die Items in der Szene
    #endregion
    public void DecreaseItemCount()
    {
        if (inventorySlot.item != null)
        {
            inventorySlot.RemoveItem(inventorySlot.item);
            removeItemAudioSource.Play();
            UpdateItemLaunchers();
        }

        UpdateRemoveItemInstructionColor();
        SetInstructionColor("ItemInstruction", Color.white);
        SetInstructionColor("AutoCollectItems", Color.white);
    }

    #region UpdateItemLaunchers Kommentare
    //Aktualisiert die Verfügbarkeit aller Items in der Szene
    #endregion
    private void UpdateItemLaunchers()
    {
        foreach (var itemLauncherItem in FindObjectsOfType<ItemLauncherItem>())
        {
            itemLauncherItem.CheckCanBePickedUp();
        }
    }

    #region UpdateRemoveItemInstructionColor Kommentare
    //Aktualisiert die Farbe der Entfernen-Anweisung basierend auf der Anzahl der Slots im Inventar
    #endregion
    private void UpdateRemoveItemInstructionColor()
    {
        SetInstructionColor("RemoveItemInstruction", inventoryManager.CheckFullSlotsCount() == 0 ? Color.gray : Color.white);
    }

    #region SetInstructionColor Kommentare
    //Ändert die Farbe der Anweisung
    #endregion
    private void SetInstructionColor(string instructionName, Color color)
    {
        GameObject.Find(instructionName).GetComponent<TextMeshProUGUI>().color = color;
    }
}