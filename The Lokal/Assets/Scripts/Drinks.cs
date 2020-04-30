using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drinks : MonoBehaviour
{
    bool isTrigger;
    bool isServed;
    SpriteRenderer thisGameObject;
    [SerializeField]
    private Order whatKindOfDrink;
    [SerializeField]
    Sprite spriteOrder;
    PositionDrinks InitialPosition;
    public bool isGrabable;

    private void Awake()
    {
        thisGameObject = gameObject.GetComponent<SpriteRenderer>();
        thisGameObject.color = new Color(1, 1, 1, 0.3f);
        InitialPosition = Cashier.current;
    }
    private void Update()
    {
        if (Input.GetKeyDown("space") && isGrabable && UIInventory.thisUIInventory.GetEmptySlot() >= 0)
        {
            isGrabable = true;
            UIInventory.addOrder(spriteOrder);
            InitialPosition.IsAvailable = true;
            Destroy(this.gameObject);
        }
        if (isGrabable)
        {
            thisGameObject.color = new Color(1, 1, 1, 1f);
        }
        else
        {
            thisGameObject.color = new Color(1, 1, 1, .4f);
        }
    }
}
