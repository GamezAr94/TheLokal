using System;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCustomers : MonoBehaviour
{
    [SerializeField]
    private GameObject Customer;

    [SerializeField]
    private Vector3[] spawns = new Vector3[3];
    private int randomPosition;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            randomPosition = UnityEngine.Random.Range(0, 3);
            Instantiate(Customer, spawns[randomPosition], Quaternion.identity);
        }
    }
}
