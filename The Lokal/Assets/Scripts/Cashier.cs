using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cashier : MonoBehaviour
{
    private static bool isTakingAnOrder = false;

    public static bool IsTakingAnOrder { get => isTakingAnOrder; set => isTakingAnOrder = value; }

}
