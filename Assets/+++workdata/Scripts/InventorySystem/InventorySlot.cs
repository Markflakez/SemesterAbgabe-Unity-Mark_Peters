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
    public Item scriptableItem;

    public void AddItem(Item newItem)
    {
        scriptableItem = newItem;
        itemNameText.text = newItem.itemName;
        itemCountText.text = newItem.itemCount.ToString();
        itemImage.sprite = newItem.itemImage;
        gameObject.SetActive(true);
    }
}
