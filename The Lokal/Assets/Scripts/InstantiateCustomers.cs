using System;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCustomers : MonoBehaviour
{
    [SerializeField]
    private GameObject Customer;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Customer, transform.position, Quaternion.identity);
        }
    }
}
