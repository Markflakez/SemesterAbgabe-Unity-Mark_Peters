using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    #region Kommentare
    //Das sind die Eigenschaften, die ein Item zugewiesen bekommt.
    //Sie werden im Inspector angezeigt und können dort bearbeitet werden.
    //Die Eigenschaften sind: Name, Anzahl, maximale Anzahl, Bild und Sound beim Aufsammeln
    #endregion
    public string itemName;
    public int itemCount;
    public int maxStackCount = 10;
    public Sprite itemImage;
    public AudioClip collectSound;
}
