using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsObjects : MonoBehaviour
{
    [SerializeField]
    private static Vector3 cashier;
    [SerializeField]
    private static GameObject[] chairs;
    public static Vector3 Cashier { get => cashier; }
    public static GameObject[] Chairs { get => chairs; set => chairs = value; }

    private void Awake()
    {
        cashier = GameObject.Find("cashierPosition").transform.position;
        Chairs = GameObject.FindGameObjectsWithTag("Chair");
    }
}
