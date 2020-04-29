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

    private void Awake()
    {
        thisGameObject = gameObject.GetComponent<SpriteRenderer>();
        thisGameObject.color = new Color(1, 1, 1, 0.3f);
        InitialPosition = Cashier.current;
    }
    private void Update()
    {
        if (Input.GetKeyDown("space") && isTrigger && UIInventory.thisUIInventory.GetEmptySlot() >= 0)
        {
            UIInventory.addOrder(spriteOrder);
            InitialPosition.IsAvailable = true;
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && !isServed)
        {
            isTrigger = true;
            thisGameObject.color = new Color(1, 1, 1, 1f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && !isServed)
        {
            isTrigger = false;
            thisGameObject.color = new Color(1, 1, 1, 0.3f);
        }
    }
}
