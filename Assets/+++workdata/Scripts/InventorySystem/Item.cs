using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    #region Kommentare
    //Das sind Eigenschaften, die ein Item haben kann.
    //Sie werden im Inspector angezeigt und können dort bearbeitet werden.
    #endregion
    public string itemName;
    public int itemCount;
    public int maxStackCount = 10;
    public Sprite itemImage;
}
