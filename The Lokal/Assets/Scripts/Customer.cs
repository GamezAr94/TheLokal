using System;
using UnityEngine;

public class Customer : MonoBehaviour, CustomerBehavior
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private Order whatOrder;
    private bool nextInLine = false;

    public bool hasOrdered = false;
    public bool isBored;
    private bool foundAChair = false;
    private bool isSitting = false;
    private bool isDoingALine = false;

    private float timeOrdering = 3f;
    private float timeWaiting = 3f;

    private PositionsObjects emptyChairsScript;
    private GameObject chairSpot;
    private Vector3 startPosition;

    public float SpeedMovement { get => speed; }
    public Order WhatOrder { get => whatOrder; set => whatOrder = value; }
    public float TimeWaiting { get => timeWaiting; set => timeWaiting = value; }
    public bool FoundAChair { get => foundAChair; }
    public bool IsSitting { get => isSitting; set => isSitting = value; }
    public float TimeOrdering { get => timeOrdering; set => timeOrdering = value; }

    public bool IsDoingALine { get => isDoingALine;}
    public bool NextInLine { get => nextInLine; set => nextInLine = value; }

    private void Awake()
    {
        emptyChairsScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PositionsObjects>();
        startPosition = new Vector3((int)Math.Round(transform.parent.position.x), (int)Math.Round(transform.parent.position.y), 0);
        addingCustomersInLine();
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
        return startPosition;
    }


    public int GetCurrentState()
    {
        if (transform.parent.position != PositionsObjects.Cashier && !hasOrdered)
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
                Debug.Log("DESTRUCTOR");
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

    private Vector3 DoingALine()
    {
        isDoingALine = true;
        return new Vector3(-1, 0, 0);
    }
    public void WaitingForFood()
    {
        Debug.Log("ADENTRO DEL STATE 3");
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
        if (chairSpot.GetComponent<ChairsAvailability>().IsAvailable && chairSpot != null)
        {
            foundAChair = true;
            chairSpot.GetComponent<ChairsAvailability>().IsAvailable = false;
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
        chairSpot.GetComponent<ChairsAvailability>().IsAvailable = true;
        return startPosition;
    }

    public Vector3 ComingToCashier()
    {
        isDoingALine = false;
        return PositionsObjects.Cashier;
    }

    public void Ordering()
    {
        Cashier.IsTakingAnOrder = true;
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
        Cashier.TotalCustomers++;
        Cashier.inLineCustomers.Add(gameObject);
    }
}
