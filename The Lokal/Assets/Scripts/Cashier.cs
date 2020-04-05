using System.Collections.Generic;
using UnityEngine;

public class Cashier : MonoBehaviour
{
    private static bool isTakingAnOrder = false;

    private static int totalCustomers;
    public static bool IsTakingAnOrder { get => isTakingAnOrder; set => isTakingAnOrder = value; }
    public static int TotalCustomers { get => totalCustomers; set => totalCustomers = value; }

    public static List<GameObject> inLineCustomers = new List<GameObject>();

    public static bool isNextInLine = true;

    
    private int countingTheLine = 0;

    private void FixedUpdate()
    {
        CallingNextCustomer();
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(inLineCustomers.Count);
        }
    }
    void CallingNextCustomer()
    {
        if (inLineCustomers.Count > 0)
        {
            if (!isTakingAnOrder && isNextInLine)
            {
                inLineCustomers[0].GetComponent<Customer>().NextInLine = true;
                inLineCustomers[0].GetComponent<Customer>().IsDoingALine = false;

                //--------------------------------
                if (inLineCustomers.Count > 1)
                {
                    for (int i = 1; i < inLineCustomers.Count; i++)
                    {
                        inLineCustomers[i].GetComponent<Customer>().LineInMotion = true;
                        inLineCustomers[i].GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
                    }
                }
                //--------------------------------
                inLineCustomers[0].GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
                isNextInLine = false;
            }
            if(isTakingAnOrder && !isNextInLine)
            {
                isNextInLine = true;
            }
        }
    }
}
