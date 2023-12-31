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
    //Sucht und initialisiert das InventoryManager-Script.
    #endregion
    private void Awake()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    #region Start Kommentare
    //Initialisiert das ItemLauncherItem.
    //Überprüft, ob das Item zum Inventar hinzugefügt werden kann.
    #endregion
    void Start()
    {
        InitializeItemLauncher();
        CheckCanBePickedUp();
    }

    #region CheckCanBePickedUp Kommentare
    //Überprüft, ob das Item zum Inventar hinzugefügt werden kann.
    //Ändert die Farbe des Bildes basierend auf der Verfügbarkeit des Platzes im Inventar. (weiß wenn verfügbar, grau wenn nicht verfügbar)
    #endregion
    public void CheckCanBePickedUp()
    {
        image.color = inventoryManager.CanThisItemBeAddedToInventory(item) ? Color.white : new Color(.2f, .2f, .2f);
    }

    #region OnPointerClick Kommentare
    //Wird aufgerufen, wenn auf das GameObject geklickt wird.
    //Fügt das Item zum Inventar hinzu.
    #endregion
    public void OnPointerClick(PointerEventData eventData)
    {
        AddItemtoInventory();
    }

    #region AddItemtoInventory Kommentare
    //Fügt das Item zum Inventar hinzu.
    //Aktualisiert die Verfügbarkeit aller ItemLauncherItems.
    #endregion
    public void AddItemtoInventory()
    {
        inventoryManager.AddItemToInventory(item, gameObject);
        UpdateItemLaunchersAvailability();
    }



    #region InitializeItemLauncher Kommentare
    //Initialisiert das ItemLauncherItem.
    //Setzt das Sprite des Items und die Größe des Sprites.
    #endregion
    private void InitializeItemLauncher()
    {
        image.sprite = item.itemImage;
        transform.localScale = new Vector2(spriteSize, spriteSize);
    }

    #region UpdateItemLaunchersAvailability Kommentare
    //Aktualisiert die Verfügbarkeit aller ItemLauncherItems.
    #endregion
    private void UpdateItemLaunchersAvailability()
    {
        foreach (var launcherItem in FindObjectsOfType<ItemLauncherItem>())
        {
            launcherItem.CheckCanBePickedUp();
        }
    }
}
