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
    //Referenzen werden gesetzt
    //Die Farbe der Entfernen-Anweisung wird auf grau gesetzt, weil das Inventar zu Beginn leer ist
    #endregion
    public void Awake()
    {
        inventorySlot = GetComponent<InventorySlot>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        SetInstructionColor("RemoveItemInstruction", Color.gray);
    }

    #region DecreaseItemCount Kommentare
    //Entfernt ein Item aus dem Inventar und aktualisiert die Verfügbarkeit aller Items in der Szene (zeigt die Items, die aufgesammelt werden kann normal and und die anderen ausgegraut)
    //Stell die Farbe der Entfernen-Anweisung auf weiß, weil das Inventar mindestens einen freien Slot hat
    #endregion
    public void DecreaseItemCount()
    {
        if (inventorySlot.item != null)
        {
            inventorySlot.RemoveItem(inventorySlot.item);
            removeItemAudioSource.Play();
            UpdateItemLaunchers();

            UpdateRemoveItemInstructionColor();
            SetInstructionColor("ItemInstruction", Color.white);
            SetInstructionColor("AutoCollectItems", Color.white);
        }
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
    //Aktualisiert die Farbe des Items in der Szene basierend auf der Verfügbarkeit im Inventar (weiß wenn verfügbar, grau wenn nicht verfügbar)
    #endregion
    private void UpdateRemoveItemInstructionColor()
    {
        SetInstructionColor("RemoveItemInstruction", inventoryManager.CheckFullSlotsCount() == 0 ? Color.gray : Color.white);
    }

    #region SetInstructionColor Kommentare
    //Ändert die Farbe der Hilfe-Anweisungen
    #endregion
    private void SetInstructionColor(string instructionName, Color color)
    {
        GameObject.Find(instructionName).GetComponent<TextMeshProUGUI>().color = color;
    }
}