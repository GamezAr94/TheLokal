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

    static Sprite orderSprite = null;
    private void Awake()
    {
        thisUIInventory = gameObject.GetComponent<UIInventory>();
    }
    private void Update()
    {
        //if(listOrders.Count > 0 && listOrders.Count <= 5 && newOrder)
        if(newOrder && !UIisEmpty())
        {
            int i = GetEmptySlot();
            newOrder = false;
            inventoryPanels[i].enabled = true;
            inventorySlots[i].enabled = true;
            inventorySlots[i].GetComponent<Button>().enabled = true;
            inventorySlots[i].sprite = orderSprite;
            orderSprite = null;
        }
    } 

    public static void addOrder(Sprite ord)
    {
        if (orderSprite == null)
        {
            newOrder = true;
            Debug.Log(ord.ToString());
            orderSprite = ord;
        }
        else
        {
            //What to do if you can not grab any more coffe
        }
    }
    public bool UIisEmpty()
    {
        for(int i = 0; i < inventoryPanels.Length; i++)
        {
            if(inventoryPanels[i] == null)
            {
                return true;
            }
        }
        return false;
    }

    public int GetEmptySlot()
    {
        if (UIisEmpty())
        {
            return -1;
        }
        else
        {
            for (int i = 0; i < inventoryPanels.Length; i++)
            {
                if (inventorySlots[i].sprite == null)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
