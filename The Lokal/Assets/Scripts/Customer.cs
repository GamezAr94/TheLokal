using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour, CustomerBehavior
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private Order whatOrder;

    [SerializeField]
    private Vector2 nextStop;

    private bool ordering;

    public bool hasOrdered;

    private float timeOrdering = 3;




    public float SpeedMovement { get => speed; }
    public Order WhatOrder { get => whatOrder; set => whatOrder = value; }
    public Vector2 NextStop { get => nextStop; set => nextStop = value; }
    public bool Ordering { get => ordering; set => ordering = value; }
    public float TimeOrdering { get => timeOrdering; set => timeOrdering = value; }

    private Vector3 spotPosition;
    private void Start()
    {
        spotPosition = PositionsObjects.Chairs[Random.Range(0, PositionsObjects.Chairs.Length - 1)].transform.position;
        Debug.Log(spotPosition );
    }
    // Start is called before the first frame update
    void FixedUpdate()
    {
        if(GetCurrentState() == 1 && !hasOrdered){
            TimeOrdering -= Time.fixedDeltaTime;
            if(TimeOrdering <= 0)
            {
                hasOrdered = true;
            }
        }
    }

    public Vector3 GetNextStop()
    {
        if(GetCurrentState() == 0)
        {
            return PositionsObjects.Cashier;
        }else if(GetCurrentState() == 2)
        {
            return spotPosition;
        }

        return new Vector3(0,0,0);
    }

    public int GetCurrentState()
    {
        if (transform.parent.position != PositionsObjects.Cashier && !hasOrdered)
        {
            return (int)CurrentState.Comming;
        }
        else if (transform.parent.position == PositionsObjects.Cashier && !hasOrdered)
        {
            return (int)CurrentState.Ordering;
        }
        else if (hasOrdered /* y aun no esta en la posicion de su silla*/)
        {
            return (int)CurrentState.FindingSpot;
        }
        else if (hasOrdered && transform.parent.position == GetNextStop())
        {
            return (int)CurrentState.Waiting;
        }/*
        Cuando este en la posision de su silla esperando la orden esperando

        Cuando este en la posision de su silla y aun no haya terminado de comer tiempo

        cuando haya terminado y se valla 
        
        else if( )
        
         */
        return 7;
    }
}
