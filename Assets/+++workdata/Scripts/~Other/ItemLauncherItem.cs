using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ItemLauncherItem : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    [HideInInspector] public float spriteSize;
    public Image image;
    private InventoryManager inventoryManager;

    #region Awake Kommentare
    //Wird aufgerufen, wenn das GameObject aktiviert wird.
    //Initialisiert den InventoryManager.
    #endregion
    private void Awake()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    #region Start Kommentare
    //Wird aufgerufen, wenn das GameObject aktiviert wird.
    //Setzt das Sprite und die Gr��e des Sprites. �berpr�ft, ob das Item aufgenommen werden kann.
    #endregion
    void Start()
    {
        InitializeItemLauncher();
        CheckCanBePickedUp();
    }

    #region CheckCanBePickedUp Kommentare
    //�berpr�ft, ob das Item zum Inventar hinzugef�gt werden kann.
    //�ndert die Farbe des Bildes basierend auf der Verf�gbarkeit des Platzes im Inventar.
    #endregion
    public void CheckCanBePickedUp()
    {
        image.color = inventoryManager.CanThisItemBeAddedToInventory(item) ? Color.white : new Color(.2f, .2f, .2f);
    }

    #region OnPointerClick Kommentare
    //Wird aufgerufen, wenn auf das GameObject geklickt wird.
    //F�gt das Item zum Inventar hinzu und aktualisiert das Image aller Items.
    #endregion
    public void OnPointerClick(PointerEventData eventData)
    {
        inventoryManager.AddItemToInventory(item, gameObject);
        UpdateItemLaunchersAvailability();
    }

    #region InitializeItemLauncher Kommentare
    //Initialisiert das ItemLauncherItem.
    #endregion
    private void InitializeItemLauncher()
    {
        image.sprite = item.itemImage;
        transform.localScale = new Vector2(spriteSize, spriteSize);
    }

    #region UpdateItemLaunchersAvailability Kommentare
    //Aktualisiert die Verf�gbarkeit aller ItemLauncherItems.
    #endregion
    private void UpdateItemLaunchersAvailability()
    {
        foreach (var launcherItem in FindObjectsOfType<ItemLauncherItem>())
        {
            launcherItem.CheckCanBePickedUp();
        }
    }
}
