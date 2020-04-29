using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cashier : MonoBehaviour
{
    private static bool isTakingAnOrder = false;

    [SerializeField]
    private GameObject[] drinks;
    [SerializeField]
    private PositionDrinks[] positionDrinks;

    bool startOtherCoffe = true;

    private static int totalCustomers;

    public static bool IsTakingAnOrder { get => isTakingAnOrder; set => isTakingAnOrder = value; }
    public static int TotalCustomers { get => totalCustomers; set => totalCustomers = value; }

    public static List<Order> orderBills = new List<Order>();

    public static List<GameObject> inLineCustomers = new List<GameObject>();
    public static List<GameObject> waitingForTable = new List<GameObject>();

    public static bool isNextInLine = true;
    public static PositionDrinks current;
    private void Update()
    {
        if (orderBills.Count > 0)
        {
            if (startOtherCoffe && GetPositionNextDrink()!=null)
            {
                StartCoroutine(startOrder());
            }
        }
    }
    private void FixedUpdate()
    {
        CallingNextCustomer();
    }
    void CallingNextCustomer()
    {
        if (inLineCustomers.Count > 0)
        {
            if (!isTakingAnOrder && isNextInLine)
            {
                inLineCustomers[0].GetComponent<Customer>().NextInLine = true;
                inLineCustomers[0].GetComponent<Customer>().IsDoingALine = false;
                if (inLineCustomers.Count > 1)
                {
                    for (int i = 1; i < inLineCustomers.Count; i++)
                    {
                        inLineCustomers[i].GetComponent<Customer>().LineInMotion = true;
                        inLineCustomers[i].GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
                    }
                }
                inLineCustomers[0].GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
                isNextInLine = false;
            }
            if(isTakingAnOrder && !isNextInLine)
            {
                isNextInLine = true;
            }
        }
    }
    public static void OrderBills(Order order)
    {
        orderBills.Add(order);
    }

    IEnumerator startOrder()
    {
        startOtherCoffe = false;
        current = GetPositionNextDrink();
        if (current != null)
        {
            Debug.Log("Empezando el cafe " + orderBills[0]);
            int order = (int)orderBills[0];
            yield return new WaitForSeconds(3);
            Instantiate(drinks[order-1], current.PositionToPutADrink, Quaternion.identity);
            current.IsAvailable = false;
            Debug.Log("Terminando el cafe " + orderBills[0]);
            orderBills.Remove(orderBills[0]);
        }
        startOtherCoffe = true;
    }
    PositionDrinks GetPositionNextDrink()
    {
        for(int i = 0; i < positionDrinks.Length; i++)
        {
            if(positionDrinks[i].IsAvailable)
            {
                return positionDrinks[i];
            }
        }
        return null;
    }
}
