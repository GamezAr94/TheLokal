﻿using System.Collections;
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


    public bool hasOrdered;
    public bool isBored;
    private bool isWalking;

    private float timeWaiting = 0;

    [SerializeField]
    private PositionsObjects emptyChair;

    private Vector3 startPosition;

    public float SpeedMovement { get => speed; }
    public Order WhatOrder { get => whatOrder; set => whatOrder = value; }
    public Vector2 NextStop { get => nextStop; set => nextStop = value; }
    public float TimeWaiting { get => timeWaiting; set => timeWaiting = value; }
    public bool IsWalking { get => isWalking; }

    private bool isSitting = false;

    private Vector3 chairSpot;

    private void Awake()
    {
        startPosition = transform.parent.position;
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
            return PositionsObjects.Cashier;
        }else if(GetCurrentState() == 2)
        {
            GameObject availableChair = emptyChair.GetAvailableChair();
            if (availableChair != null)
            {
                isSitting = true;
                chairSpot = availableChair.transform.position;
                return availableChair.transform.position;
            }
            else
            {
                //what to do if its empty;
                Debug.Log("NADA");
                return new Vector3(0, 0, 0);
            }
        } 
        else if (GetCurrentState() == 5)
        {
            return startPosition;
        }
        return new Vector3(0, 0, 0);
    }


    public int GetCurrentState()
    {
        if (transform.parent.position != PositionsObjects.Cashier && !hasOrdered)
        {
            isWalking = true;
            return (int)CurrentState.Comming;
        }
        else if (transform.parent.position == PositionsObjects.Cashier && !hasOrdered)
        {
            isWalking = false;
            return (int)CurrentState.Ordering;
        }
        else if (hasOrdered && isSitting == false)
        {
            isWalking = true;
            return (int)CurrentState.FindingSpot;
        }
        else if (hasOrdered && isSitting == true && !isBored)
        {
            if (chairSpot == transform.parent.position )
            {
                isWalking = false;
                return (int)CurrentState.Waiting;
            }
            //agregar una condicion si ya tiene comida 
        }
        else if (isBored)
        {/*
            if(transform.parent.position == startPosition)
            {
                DestroyItself();
            }*/
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
        Destroy(this);
    }
    public void setActiveWalikng()
    {
        isWalking=true?(isWalking=false):(isWalking = true);
    }
}
