using System;
using UnityEngine;

public class Customer : MonoBehaviour, CustomerBehavior
{

    [SerializeField]
    private float speed;
    private bool hasBeenCounted = false;

    public bool isInTheCofe;

    public bool isMoving = false;


    [SerializeField]
    private Order whatOrder;


    [SerializeField]
    private bool nextInLine = true;

    public bool LineInMotion = false;

    public bool hasOrdered = false;
    public bool isBored;
    private bool foundAChair = false;
    private bool isSitting = false;
    private bool isDoingALine = false;

    [SerializeField]
    private float timeOrdering;
    [SerializeField]
    private float timeWaiting;

    private PositionsObjects emptyChairsScript;
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
        emptyChairsScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PositionsObjects>();
        startPosition = new Vector3((int)Math.Round(transform.parent.position.x), (int)Math.Round(transform.parent.position.y), 0);
    }

    public Vector3 GetNextStop()
    {
        if(GetCurrentState() == 0)
        {
            Debug.Log("ADENTRO DEL STATE 0");
            return ComingToCashier();
        }
        if(GetCurrentState() == 2)
        {
            Debug.Log("ADENTRO DEL STATE 2");
            return FindingSpot();
        } 
        if (GetCurrentState() == 5)
        {
            Debug.Log("ADENTRO DEL STATE 5");
            return GoingHome();
        }
        if(GetCurrentState() == 6)
        {
            Debug.Log("ADENTRO DEL STATE 6");
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
        else if (hasOrdered && isSitting == false)
        {
            return (int)CurrentState.FindingSpot;
        }
        else if (hasOrdered && isSitting == true && !isBored)
        {
            if (chairSpot.transform.position == transform.parent.position)
            {
                return (int)CurrentState.Waiting;
            }
            //agregar una condicion si ya tiene comida 
        }
        else if (isBored)
        {
            if (chairSpot != null && chairSpot.transform.position != transform.parent.position)
            {
                chairSpot.GetComponent<ChairsAvailability>().IsAvailable = true;
                chairSpot = null;
            }
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
            Debug.Log("Casi!");
            if (Cashier.inLineCustomers[i+1].Equals(gameObject))
            {
                Debug.Log("Bien muchachon " + GetingTileMaps.positionInLine[i]);
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
            Debug.Log("Casi!");
            if (Cashier.inLineCustomers[i].Equals(gameObject))
            {
                Debug.Log("Bien");
                return GetingTileMaps.positionInLine[i];
            }
        }
        Debug.Log("EROOOOOR in doing a line method");
        return new Vector3(-1f, 0f, 0f);
    }
    public void WaitingForFood()
    {
        Debug.Log("ADENTRO DEL STATE 3");
        if (chairSpot.GetComponent<ChairsAvailability>().IsAvailable)
        {
            isMoving = false;
            chairSpot.GetComponent<ChairsAvailability>().IsAvailable = false;
        }
        timeWaiting -= Time.fixedDeltaTime;
        //añadir a la condicion si aun no tiene comida 
        if (timeWaiting <= 0)
        {
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
            foundAChair = true;
            isMoving = true;
            return chairSpot.transform.position;
        }
        else
        {
            //what to do if its empty;
            Debug.Log("NADA de sillas");
            return new Vector3(-1, 0, 0);
        }
    }

    public Vector3 GoingHome()
    {
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
        Debug.Log("ADENTRO DEL STATE 1");
        timeOrdering -= Time.fixedDeltaTime;
        Debug.Log(Cashier.IsTakingAnOrder);
        if (timeOrdering <= 0)
        {
            Cashier.IsTakingAnOrder = false;
            Debug.Log(Cashier.IsTakingAnOrder);
            hasOrdered = true;
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
