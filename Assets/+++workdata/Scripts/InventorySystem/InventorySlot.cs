using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemCountText;
    public Image itemImage;
    public Item item;

    #region Kommentare
    //UpdateSlotUI wird aufgerufen um die UI zu aktualisieren heißt die UI Elemente werden deaktiviert wenn kein Item im Slot ist
    #endregion
    private void Start()
    {
        UpdateSlotUI();
    }

    #region Kommentare
    //Das Item wird dem Slot zugewiesen und die Funktion UpdateSlotUI() wird aufgerufen um die UI Elemente zu aktualisieren
    #endregion
    public void AddItem(Item newItem)
    {
        item = newItem;
        UpdateSlotUI();
    }

    #region Kommentare
    //Die Anzahl des Items wird um den Wert "amount" erhöht
    //Wenn die Anzahl des Items größer als der Maximalwert ist, wird die Anzahl der Items auf den Maximalwert gesetzt
    //Die Funktion UpdateSlotUI() wird aufgerufen um die UI Elemente zu aktualisieren
    #endregion
    public void IncreaseItemCount(int amount)
    {
        if (item != null)
        {
            int totalItemCount = item.itemCount + amount;

            if (totalItemCount > item.maxStackCount)
            {
                amount = item.maxStackCount - item.itemCount;
            }

            item.itemCount += amount;

            UpdateSlotUI();
        }
    }

    #region Kommentare
    //Wenn Item nicht null ist, überschreiben die Werte des Items die Werte der UI Elemente
    //Außerdem werden die UI Elemente aktiviert
    //Wenn Item null ist, werden die Werte der UI Elemente wieder auf den Standardwert gesetzt
    //Außerdem werden die UI Elemente deaktiviert
    #endregion
    private void UpdateSlotUI()
    {
        if (item != null)
        {
            UpdadeUIElements();
        }
        else
        {
            ResetUIElements();
        }
    }

    private void UpdadeUIElements()
    {
        itemNameText.text = item.itemName;
        itemCountText.text = item.itemCount.ToString();
        itemImage.sprite = item.itemImage;

        itemNameText.enabled = true;
        itemCountText.enabled = true;
        itemImage.enabled = true;
    }

    private void ResetUIElements()
    {
        itemNameText.text = "";
        itemCountText.text = "";
        itemImage.sprite = null;

        itemNameText.enabled = false;
        itemCountText.enabled = false;
        itemImage.enabled = false;
    }



}