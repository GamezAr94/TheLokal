using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionDrinks : MonoBehaviour
{
    [SerializeField]
    bool isAvailable;
    [SerializeField]
    Vector3 positionToPutADrink;


    public bool IsAvailable { get => isAvailable; set => isAvailable = value; }
    public Vector3 PositionToPutADrink { get => positionToPutADrink; }

    void Awake()
    {
        isAvailable = true;
        positionToPutADrink = gameObject.transform.position;
    }
}
