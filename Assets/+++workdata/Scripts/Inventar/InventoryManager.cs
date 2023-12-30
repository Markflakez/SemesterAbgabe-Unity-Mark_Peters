using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using DG.Tweening;
public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> inventorySlots;
    public AudioSource audioSource;
    public HealthSystem healthSystem;



    #region Update Kommentare
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            foreach (ItemLauncherItem itemLauncheritem in FindObjectsOfType<ItemLauncherItem>())
            {
                itemLauncheritem.AddItemtoInventory();
            }
        }
    }


    #region AddItemToInventory Kommentare
    //F�gt ein Item zum Inventar hinzu
    //Wenn das Item bereits im Inventar ist, wird die Anzahl erh�ht
    //Wenn das Item nicht im Inventar ist, wird ein neuer Slot erstellt
    //Wenn das Item nicht im Inventar ist und kein Slot mehr frei ist, wird die Anzahl des Items im Slot erh�ht
    #endregion

    public void AddItemToInventory(Item newItem, GameObject itemToDestroy)
    {
        InventorySlot existingSlot = GetExistingSlot(newItem);

        if (existingSlot != null)
            HandleExistingSlot(existingSlot, newItem, itemToDestroy);
        else
            HandleNewSlot(newItem, itemToDestroy);

        UpdateInstructionColors();
    }

    #region RemoveItemFromInventory Kommentare
    //Entfernt ein Item aus dem Inventar
    //Wenn die Anzahl des Items im Slot gr��er als 1 ist, wird die Anzahl verringert
    //Wenn die Anzahl des Items im Slot 1 ist, wird der Slot geleert
    #endregion
    private void HandleExistingSlot(InventorySlot existingSlot, Item newItem, GameObject itemToDestroy)
    {
        existingSlot.IncreaseItemCount(newItem.itemCount);
        HandleItemCollection(itemToDestroy, newItem.collectSound);
    }

    #region HandleNewSlot Kommentare
    //Erstellt einen neuen Slot und f�gt das Item hinzu
    //Wenn kein Slot mehr frei ist, wird die Anzahl des Items im Slot erh�ht
    #endregion
    private void HandleNewSlot(Item newItem, GameObject itemToDestroy)
    {
        InventorySlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            Item clonedItem = Instantiate(newItem);
            emptySlot.AddItem(clonedItem);
            HandleItemCollection(itemToDestroy, newItem.collectSound);
        }
        else
        {
            HandleNoEmptySlot(newItem);
        }
    }

    #region HandleItemCollection Kommentare
    //Zerst�rt das Item und spielt den Sound ab
    #endregion
    private void HandleItemCollection(GameObject itemToDestroy, AudioClip collectSound)
    {
        itemToDestroy.transform.DOScale(itemToDestroy.transform.localScale * 0.5f, 0.125f).OnComplete(() => Destroy(itemToDestroy));
        audioSource.clip = collectSound;
        audioSource.Play();
    }
    #region HandleNoEmptySlot Kommentare
    //Wenn kein Slot mehr frei ist, wird die Anzahl des Items im Slot erh�ht
    #endregion
    private void HandleNoEmptySlot(Item newItem)
    {
        InventorySlot emptySlot = GetSlotWithSameItem(newItem);

        if (emptySlot != null && emptySlot.item.itemCount < emptySlot.item.maxStackCount)
        {
            emptySlot.IncreaseItemCount(newItem.itemCount);
            HandleItemCollection(null, newItem.collectSound);
        }
        else
        {
            Debug.LogWarning("Inventory full, cannot add more of this item.");
        }
    }


    #region UpdateInstructionColors Kommentare
    //Aktualisiert die Farben der Instructions (Spielerhilfen) basierend auf der Anzahl der Slots im Inventar
    #endregion
    private void UpdateInstructionColors()
    {
        TextMeshProUGUI itemInstruction = GameObject.Find("ItemInstruction").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI AutoCollectItems = GameObject.Find("AutoCollectItems").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI removeItemInstruction = GameObject.Find("RemoveItemInstruction").GetComponent<TextMeshProUGUI>();

        itemInstruction.color = CheckFullSlotsCount() == inventorySlots.Count ? Color.gray : itemInstruction.color;
        AutoCollectItems.color = CheckFullSlotsCount() == inventorySlots.Count ? Color.gray : AutoCollectItems.color;
        removeItemInstruction.color = CheckEmptySlotsCount() != 0 ? Color.white : removeItemInstruction.color;
    }

    #region CheckFullSlotsCount Kommentare
    //�berpr�ft die Anzahl der vollen Slots im Inventar
    #endregion
    public int CheckFullSlotsCount() => inventorySlots.Count(slot => slot.item != null && slot.item.itemCount == slot.item.maxStackCount);

    #region CheckEmptySlotsCount Kommentare
    //�berpr�ft die Anzahl der leeren Slots im Inventar
    #endregion
    public int CheckEmptySlotsCount() => inventorySlots.Count(slot => slot.item == null);

    #region CanThisItemBeAddedToInventory Kommentare
    //�berpr�ft, ob ein Item zum Inventar hinzugef�gt werden kann
    #endregion
    public bool CanThisItemBeAddedToInventory(Item item) => GetExistingSlot(item) != null || GetEmptySlot() != null || (GetSlotWithSameItem(item)?.item.itemCount < GetSlotWithSameItem(item)?.item.maxStackCount);

    #region GetEmptySlot Kommentare
    //Sucht nach einem leeren Slot und gibt diesen zur�ck
    #endregion
    private InventorySlot GetEmptySlot() => inventorySlots.FirstOrDefault(slot => slot.item == null);

    #region GetExistingSlot Kommentare
    //�berpr�ft, ob es einen Slot gibt, der das Item enth�lt, und gibt diesen zur�ck
    #endregion
    private InventorySlot GetExistingSlot(Item newItem) => inventorySlots.FirstOrDefault(slot => slot.item != null && slot.item.itemName == newItem.itemName && slot.item.itemCount < slot.item.maxStackCount);

    #region GetSlotWithSameItem Kommentare
    //Gibt den ersten Slot zur�ck, der das gleiche Item enth�lt
    #endregion
    private InventorySlot GetSlotWithSameItem(Item newItem) => inventorySlots.FirstOrDefault(slot => slot.item != null && slot.item.itemName == newItem.itemName);
}