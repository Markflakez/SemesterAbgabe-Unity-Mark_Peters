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
    //Startmethode wird aufgerufen, um das den Slot und seine UI-Elemente zu initialisieren und zu aktualisieren
    #endregion
    private void Start()
    {
        UpdateSlotUI();
    }

    #region AddItem Kommentare
    //Weist dem Slot ein neues Item zu und aktualisiert die UI-Elemente
    #endregion
    public void AddItem(Item newItem)
    {
        item = newItem;
        UpdateSlotUI();
    }

    #region RemoveItem Kommentare
    //Entfernt ein bestimmtes Item aus dem Slot und aktualisiert die UI-Elemente
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
    //Erhöht die Anzahl des Items im Slot und aktualisiert die UI-Elemente
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
    //Aktualisiert die UI-Elemente vom Slot basierend auf dem aktuell hinzugefügten Item
    //Wenn kein Item vorhanden ist, werden die UI-Elemente zurückgesetzt
    //Wenn die Anzahl des Items im Slot gleich der maximalen Anzahl ist, wird die Anzahl rot angezeigt um den Spieler darauf hinzuweisen, dass er kein weiteres Item mehr aufnehmen kann
    //Wenn die Anzahl des Items im Slot kleiner der maximalen Anzahl ist, wird die Anzahl weiß angezeigt um den Spieler darauf hinzuweisen, dass der Slot noch nicht voll ist
    #endregion
    private void UpdateSlotUI()
    {
        if (item != null) UpdadeUIElements();
        else ResetUIElements();

        if (item != null && itemCountText != null)
            itemCountText.color = item.itemCount < item.maxStackCount ? Color.white : Color.red;
    }

    #region UpdadeUIElements Kommentare
    //Aktualisiert die UI-Elemente mit den Werten des aktuellen Items
    //Wenn die Anzahl des Items im Slot gleich ist mit dem Maximalwert des Items, wird das Item dunkel angezeigt um den Spieler darauf hinzuweisen, dass er kein weiteres Item mehr aufnehmen kann
    //Wenn die Anzahl des Items im Slot kleiner ist als der Maximalwert des Items, wird das Item normal angezeigt um den Spieler darauf hinzuweisen, dass der Slot noch nicht voll ist
    #endregion
    private void UpdadeUIElements()
    {
        itemNameText.text = item.itemName;
        itemCountText.text = item.itemCount.ToString();
        itemImage.sprite = item.itemImage;

        if(item.itemCount == item.maxStackCount)
        {
            itemImage.color = new Color(.2f, .2f, .2f);
        }
        else
        {
            itemImage.color = Color.white;
        }

        EnableUIElements();
        AnimateItemImageScale();
    }

    #region ResetUIElements Kommentare
    //Setzt die UI-Elemente auf Standardwerte zurück
    //Deaktiviert die UI-Elemente
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
    //Skaliert das Item Bild um 20% kleiner und wieder zurück um dem Spieler visuell darzustellen, dass er ein Item aufgenommen hat (feedback)
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