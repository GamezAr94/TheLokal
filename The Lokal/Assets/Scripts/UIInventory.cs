using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class UIInventory : MonoBehaviour
{
    [SerializeField]
    Image[] inventorySlots;
    [SerializeField]
    Image[] inventoryPanels;
    [SerializeField]
    Sprite[] drinksSprites;
    static bool newOrder = false;
    static bool full = false;
    public static UIInventory thisUIInventory;

    static List<SpriteRenderer> listOrders = new List<SpriteRenderer>();
    private void Awake()
    {
        thisUIInventory = gameObject.GetComponent<UIInventory>();
    }
    private void Update()
    {
        if(listOrders.Count > 0 && listOrders.Count <= 5 && newOrder)
        {
            int i = GetEmptySlot();
            newOrder = false;
            inventoryPanels[i].enabled = true;
            inventorySlots[i].enabled = true;
            inventorySlots[i].GetComponent<Button>().enabled = true;
            inventorySlots[i].sprite = drinksSprites[0];
        }
    }

    public static void addOrder(SpriteRenderer ord)
    {
        newOrder = true;
        listOrders.Add(ord);
    }

    public int GetEmptySlot()
    {
        for(int i = 0; i < inventoryPanels.Length; i++)
        {
            if (inventorySlots[i].sprite == null)
            {
                return i;
            }
        }
        return -1;
    }
}
