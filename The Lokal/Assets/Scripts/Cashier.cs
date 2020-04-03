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

    public static bool lineIsInMovement = false;
    
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
                inLineCustomers[countingTheLine].GetComponent<Customer>().NextInLine = true;

                //--------------------------------
                for (int i = 0; i < GetingTileMaps.positionInLine.Count && i < inLineCustomers.Count-1; i++)
                {
                    inLineCustomers[i+1].GetComponent<Customer>().LineInMotion = true;
                }
                //--------------------------------
                inLineCustomers[0].GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);

                isNextInLine = false;
                lineIsInMovement = true;
            }
            if(isTakingAnOrder && !isNextInLine)
            {
                isNextInLine = true;
            }
        }
    }
}
