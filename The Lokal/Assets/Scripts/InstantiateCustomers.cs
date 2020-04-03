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
        if (Input.GetMouseButtonDown(0) && Cashier.inLineCustomers.Count < GetingTileMaps.positionInLine.Count-1 )
        {
            for (int i = 0; i < 2; i++)
            {
                randomPosition = Random.Range(0, 3);
                Instantiate(Customer, spawns[randomPosition], Quaternion.identity);
            }
        }
    }
}
