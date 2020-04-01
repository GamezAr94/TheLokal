using System;
using UnityEngine;

public class Customer : MonoBehaviour, CustomerBehavior
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private Order whatOrder;

    public bool hasOrdered;
    public bool isBored;
    private bool isWalking;

    private float timeWaiting = 0;

    private PositionsObjects emptyChair;

    private Vector3 startPosition;

    public float SpeedMovement { get => speed; }
    public Order WhatOrder { get => whatOrder; set => whatOrder = value; }
    public float TimeWaiting { get => timeWaiting; set => timeWaiting = value; }
    public bool IsWalking { get => isWalking; }

    private bool isSitting = false;

    private GameObject chairSpot;

    private void Awake()
    {
        emptyChair = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PositionsObjects>();
        startPosition = new Vector3((int)Math.Round(transform.parent.position.x), (int)Math.Round(transform.parent.position.y), 0);
    }
    
    // Start is called before the first frame update
    void FixedUpdate()
    {
        if(GetCurrentState() == 1 && !hasOrdered){
            timeWaiting += Time.fixedDeltaTime;
            if(timeWaiting >= 5)
            {
                hasOrdered = true;
                TimeWaiting = 0;
            }
        }else if(GetCurrentState() == 3)
        {
            timeWaiting += Time.fixedDeltaTime;
            //añadir a la condicion si aun no tiene comida 
            if (timeWaiting >= 5)
            {
                isBored = true;
                TimeWaiting = 0;
            }
        }
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
        Debug.Log("Posible Bug");
        return startPosition;
    }


    public int GetCurrentState()
    {
        if (transform.parent.position != PositionsObjects.Cashier && !hasOrdered)
        {
            if (Cashier.IsTakingAnOrder == false)
            {
                isWalking = true;
                return (int)CurrentState.Comming;
            }
            else
            {
                isWalking = false;
            }
        }
        else if (transform.parent.position == PositionsObjects.Cashier && !hasOrdered)
        {
            Cashier.IsTakingAnOrder = true;
            isWalking = false;
            return (int)CurrentState.Ordering;
        }
        else if (hasOrdered && isSitting == false)
        {
            Cashier.IsTakingAnOrder = false;
            isWalking = true;
            return (int)CurrentState.FindingSpot;
        }
        else if (hasOrdered && isSitting == true && !isBored)
        {
            if (chairSpot.transform.position == transform.parent.position )
            {
                isWalking = false;
                return (int)CurrentState.Waiting;
            }
            //agregar una condicion si ya tiene comida 
        }
        else if (isBored)
        {
            if(transform.parent.position == startPosition)
            {
                DestroyItself();
            }
            isWalking = true;
            return (int)CurrentState.Going;
        }/*
        Cuando este en la posision de su silla esperando la orden esperando

        Cuando este en la posision de su silla y aun no haya terminado de comer tiempo

        cuando haya terminado y se valla 
        
        else if( )
        
         */
        return 7;
    }

    public void DestroyItself()
    {
        Destroy(transform.parent.gameObject);
        Destroy(transform.gameObject);
    }

    public Vector3 FindingSpot()
    {
        chairSpot = emptyChair.GetAvailableChair();
        if (chairSpot != null)
        {
            isSitting = true;
            return chairSpot.transform.position;
        }
        else
        {
            //what to do if its empty;
            Debug.Log("NADA");
            return new Vector3(0, 0, 0);
        }
    }

    public Vector3 GoingHome()
    {
        chairSpot.GetComponent<ChairsAvailability>().setAvailable();
        return startPosition;
    }

    public Vector3 ComingToCashier()
    {
        return PositionsObjects.Cashier;
    }
}
