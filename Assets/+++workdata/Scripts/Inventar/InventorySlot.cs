using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class InventorySlot : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemCountText;
    public Image itemImage;
    public Item item;

    #region Start Kommentare
    //Startmethode wird aufgerufen, um das UI zu initialisieren und zu aktualisieren
    #endregion
    private void Start()
    {
        UpdateSlotUI();
    }

    #region AddItem Kommentare
    //Weist dem Slot ein neues Item zu und aktualisiert das UI
    #endregion
    public void AddItem(Item newItem)
    {
        item = newItem;
        UpdateSlotUI();
    }

    #region RemoveItem Kommentare
    //Entfernt ein bestimmtes Item aus dem Slot und aktualisiert das UI
    #endregion
    public void RemoveItem(Item itemToRemove)
    {
        if (item == itemToRemove)
        {
            item = null;
            UpdateSlotUI();
        }
    }

    #region IncreaseItemCount Kommentare
    //Erhöht die Anzahl des Items im Slot und aktualisiert das UI
    #endregion
    public void IncreaseItemCount(int amount)
    {
        if (item != null)
        {
            int totalItemCount = item.itemCount + amount;
            if (totalItemCount > item.maxStackCount) amount = item.maxStackCount - item.itemCount;
            item.itemCount += amount;
            UpdateSlotUI();
        }
    }

    #region UpdateSlotUI Kommentare
    //Aktualisiert den UI Slot basierend auf dem aktuellen Item
    #endregion
    private void UpdateSlotUI()
    {
        if (item != null) UpdadeUIElements();
        else ResetUIElements();

        if (item != null && itemCountText != null)
            itemCountText.color = item.itemCount < item.maxStackCount ? Color.white : Color.black;
    }

    #region UpdadeUIElements Kommentare
    //Aktualisiert die UI-Elemente mit den Werten des aktuellen Items
    #endregion
    private void UpdadeUIElements()
    {
        itemNameText.text = item.itemName;
        itemCountText.text = item.itemCount.ToString();
        itemImage.sprite = item.itemImage;

        EnableUIElements();
        AnimateItemImageScale();
    }

    #region ResetUIElements Kommentare
    //Setzt die UI-Elemente auf Standardwerte zurück, wenn kein Item vorhanden ist
    #endregion
    private void ResetUIElements()
    {
        ClearUIElements();
        DisableUIElements();
    }

    #region EnableUIElements Kommentare
    //Aktiviert die UI-Elemente
    #endregion
    private void EnableUIElements()
    {
        itemNameText.enabled = true;
        itemCountText.enabled = true;
        itemImage.enabled = true;
    }

    #region AnimateItemImageScale Kommentare
    //Skaliert das Item Bild um 20% kleiner und wieder zurück
    #endregion
    private void AnimateItemImageScale() => itemImage.transform.DOScale(1.2f, 0.15f).SetLoops(2, LoopType.Yoyo);


    #region ClearUIElements Kommentare
    //Setzt die UI-Elemente zurück
    #endregion
    private void ClearUIElements()
    {
        itemNameText.text = "";
        itemCountText.text = "";
        itemImage.sprite = null;
    }

    #region DisableUIElements Kommentare
    //Deaktiviert die UI-Elemente
    #endregion
    private void DisableUIElements()
    {
        itemNameText.enabled = false;
        itemCountText.enabled = false;
        itemImage.enabled = false;
    }
}