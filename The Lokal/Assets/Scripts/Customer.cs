using System;
using UnityEngine;

public class Customer : MonoBehaviour, CustomerBehavior
{

    [SerializeField]
    private float speed;
    private bool hasBeenCounted = false;

    public bool isInTheCofe;

    public bool isMoving = false;

    public bool waitingTable;


    [SerializeField]
    private Order whatOrder;


    [SerializeField]
    private bool nextInLine = true;

    public bool LineInMotion = false;

    [SerializeField]
    public bool hasOrdered = false;
    public bool isBored;
    private bool foundAChair = false;
    private bool isSitting = false;
    [SerializeField]
    private bool isDoingALine = false;

    [SerializeField]
    private float timeOrdering;
    [SerializeField]
    private float timeWaiting;

    private PositionsObjects emptyChairsScript = PositionsObjects.GameManagerPositions;
    private GameObject chairSpot;
    private Vector3 startPosition;

    public float SpeedMovement { get => speed; }
    public Order WhatOrder { get => whatOrder; set => whatOrder = value; }
    public float TimeWaiting { get => timeWaiting; set => timeWaiting = value; }
    public bool FoundAChair { get => foundAChair; }
    public bool IsSitting { get => isSitting; set => isSitting = value; }
    public float TimeOrdering { get => timeOrdering; set => timeOrdering = value; }

    public bool IsDoingALine { get => isDoingALine; set => isDoingALine = value; }
    public bool NextInLine { get => nextInLine; set => nextInLine = value; }
    public GameObject ChairSpot { get => chairSpot; set => chairSpot = value; }


    private void Awake()
    {
        whatOrder = (Order)UnityEngine.Random.Range(1, 5);
        startPosition = transform.parent.position;
    }

    public Vector3 GetNextStop()
    {
        if(GetCurrentState() == 0)
        {
            return ComingToCashier();
        }
        if(GetCurrentState() == 2)
        {
            return FindingSpot();
        } 
        if (GetCurrentState() == 5)
        {
            return GoingHome();
        }
        if(GetCurrentState() == 6)
        {
            return DoingALine();
        }
        Debug.Log("Posible Bug");
        return ComingToCashier();
    }


    public int GetCurrentState()
    {
        if (transform.parent.position != PositionsObjects.Cashier && !hasOrdered && !LineInMotion && !isDoingALine)
        {
            if (nextInLine)
            {
                return (int)CurrentState.Comming;
            }
            else
            {
                return (int)CurrentState.Line;
            }
        }
        if (!hasOrdered && isDoingALine && LineInMotion && !isMoving && transform.parent.position != PositionsObjects.Cashier)
        {
            return (int)CurrentState.MovingTheLine;
        }


        else if (transform.parent.position == PositionsObjects.Cashier && !hasOrdered)
        {
            return (int)CurrentState.Ordering;
        }
        else if (hasOrdered && !isSitting)
        {
            return (int)CurrentState.FindingSpot;
        }
        else if (hasOrdered && isSitting && !isBored)
        {
            if (chairSpot.transform.position == transform.parent.position)
            {
                return (int)CurrentState.Waiting;
            }
            //agregar una condicion si ya tiene comida 
        }
        else if (isBored)
        {
            if (transform.parent.position == startPosition)
            {
                DestroyItself();
            }
            return (int)CurrentState.Going;
        }/*
        Cuando este en la posision de su silla esperando la orden esperando

        Cuando este en la posision de su silla y aun no haya terminado de comer tiempo

        cuando haya terminado y se valla 
        
        else if( )
        
         */
        return 7;
    }

    public Vector3 TheLineIsMoving()
    {
        LineInMotion = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);

        for (int i = 0; i < GetingTileMaps.positionInLine.Count && i < Cashier.inLineCustomers.Count-1; i++)
        {
            if (Cashier.inLineCustomers[i+1].Equals(gameObject))
            {
                return GetingTileMaps.positionInLine[i];
            }
        }
        return new Vector3(-1f, 0f, 0f);
    }

    public Vector3 DoingALine()
    {
        Cashier.inLineCustomers.Add(gameObject);
        isDoingALine = true;
        isMoving = false;
        for (int i = 0; i < GetingTileMaps.positionInLine.Count && i < Cashier.inLineCustomers.Count; i++)
        {
            if (Cashier.inLineCustomers[i].Equals(gameObject))
            {
                return GetingTileMaps.positionInLine[i];
            }
        }
        return new Vector3(-1f, 0f, 0f);
    }
    public void WaitingForFood()
    {
        timeWaiting -= Time.fixedDeltaTime;
        //añadir a la condicion si aun no tiene comida 
        if (timeWaiting <= 0)
        {
            isMoving = false;
            isBored = true;
        }
    }

    public void DestroyItself()
    {
        Destroy(transform.parent.gameObject);
        Destroy(transform.gameObject);
    }

    public Vector3 FindingSpot()
    {
        chairSpot = emptyChairsScript.GetAvailableChair();
        if (chairSpot != null)
        {
            waitingTable = false;
            isDoingALine = false;
            foundAChair = true;
            isMoving = true; 
            chairSpot.GetComponent<ChairsAvailability>().IsAvailable = false;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
            return chairSpot.transform.position;
        }
        else
        {
            isDoingALine = true;
            waitingTable = true;
            isMoving = false;
            Cashier.waitingForTable.Add(gameObject);
            Debug.Log("Total of peaople waiting for tables: " + Cashier.waitingForTable.Count);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
            return new Vector3(-2, -1, 0);
        }
    }

    public Vector3 GoingHome()
    {
        if (chairSpot != null)
        {
            chairSpot.GetComponent<ChairsAvailability>().IsAvailable = true;
            chairSpot = null;
        }
        if (Cashier.waitingForTable.Count > 0)
        {
            Cashier.waitingForTable[0].GetComponent<Customer>().waitingTable = false;
            Cashier.waitingForTable.RemoveAt(0);
        }
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f);
        isMoving = true;
        return startPosition;
    }

    public Vector3 ComingToCashier()
    {
        if (Cashier.IsTakingAnOrder && isInTheCofe)
        {
            nextInLine = false;
        }
        if (isInTheCofe)
        {
            isMoving = true;
            Cashier.IsTakingAnOrder = true;
            if (Cashier.inLineCustomers.Count > 0)
            {
                Cashier.inLineCustomers.Remove(gameObject);
            }
            return PositionsObjects.Cashier;
        }
        else
        {
            isMoving = true;
            return new Vector3(-1f, 0f, 0f);
        }
    }

    public void Ordering()
    {
        isMoving = false;
        timeOrdering -= Time.fixedDeltaTime;
        if (timeOrdering <= 0)
        {
            Cashier.IsTakingAnOrder = false;
            hasOrdered = true;
            Cashier.OrderBills(whatOrder);
            if(Cashier.waitingForTable.Count > 0)
            {
                isDoingALine = true;
            }
        }
    }

    public void addingCustomersInLine()
    {
        hasBeenCounted = true;
        isMoving = false;
        isInTheCofe = true;
        Cashier.TotalCustomers++;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Entrance") && !hasBeenCounted)
        {
            addingCustomersInLine();
        }
    }
}
