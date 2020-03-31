using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsObjects : MonoBehaviour
{
    [SerializeField]
    private static Vector3 cashier;
    public static Vector3 Cashier { get => cashier; }


    private static GameObject[] chairsArrayList = new GameObject[10];
    public static GameObject[] ChairsArrayList { get => chairsArrayList; set => chairsArrayList = value; }




    //Instantiate all the fields
    private void Awake()
    {
        cashier = GameObject.Find("cashierPosition").transform.position;

        ChairsArrayList = GameObject.FindGameObjectsWithTag("Chair");
        
    }

    public GameObject GetAvailableChair()
    {
        bool findIt = false;
        int i = 0;
        while(!findIt && i < ChairsArrayList.Length)
        {
            ChairsAvailability chairScript = ChairsArrayList[i].GetComponent<ChairsAvailability>();
            if (ChairsArrayList[i] != null && chairScript != null && chairScript.IsAvailable == true)
            {
                chairScript.setAvailable();
                findIt = true;
                return ChairsArrayList[i];
            }
            else
            {
                i++;
                Debug.Log("the script is null");
                //agregar que camine aleatoriamente porque no haylugares
            }
        }
        return null;
    }
}
