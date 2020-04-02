using System.Collections.Generic;
using UnityEngine;

public class Cashier : MonoBehaviour
{
    private static bool isTakingAnOrder = false;

    private static int totalCustomers;
    public static bool IsTakingAnOrder { get => isTakingAnOrder; set => isTakingAnOrder = value; }
    public static int TotalCustomers { get => totalCustomers; set => totalCustomers = value; }

    public static List<GameObject> inLineCustomers = new List<GameObject>();

    public bool isNextInLine = true;
    
    private int countingTheLine = 0;

    private void FixedUpdate()
    {
        CallingNextCustomer();
    }
    void CallingNextCustomer()
    {
        if (inLineCustomers.Count > 0)
        {
            if (countingTheLine < inLineCustomers.Count)
            {
                if (!isTakingAnOrder && isNextInLine)
                {
                    inLineCustomers[countingTheLine].GetComponent<Customer>().NextInLine = true;
                    countingTheLine++;
                    isNextInLine = false;
                }
            }
            if(isTakingAnOrder && !isNextInLine)
            {
                isNextInLine = true; 
            }
        }
    }
}
