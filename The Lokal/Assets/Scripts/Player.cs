using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Astar astar;

    private Stack<Vector3> path;

    private Vector3 destination;

    private Vector3 goal;

    [SerializeField]
    private Customer customer;

    int currentCustomerState;
    public Vector3 Goal { get => goal; set => goal = value; }

    private void Update()
    {
        currentCustomerState = customer.GetCurrentState();
    }
    public void FixedUpdate()
    {
        switch (currentCustomerState)
        {
            case 0:
                if (!customer.isMoving)
                {
                    GetPath(customer.GetNextStop());
                }
                break;
            case 1:
                customer.Ordering();
                break;
            case 2:
                if (!customer.waitingTable)
                {
                    if (!customer.FoundAChair)
                    {
                        GetPath(customer.GetNextStop());
                    }
                    if (path == null)
                    {
                        if (!customer.waitingTable)
                        {
                            customer.IsSitting = true;
                        }
                    }
                }
                break;
            case 3:
                customer.WaitingForFood();
                break;
            case 5:
                if (!customer.waitingTable && path == null && !customer.isMoving)
                {
                    GetPath(customer.GetNextStop());
                }
                break;
            case 6:
                if (!customer.IsDoingALine)
                {
                    GetPath(customer.GetNextStop());
                }
                break;
            case 7:
                if (customer.LineInMotion && !customer.isMoving && gameObject.transform.parent.position != customer.TheLineIsMoving() && !customer.hasOrdered)
                {
                    GetPath(customer.TheLineIsMoving());
                }
                break;
            default:
                Debug.Log("ERRRRROOOOOOORRR! " + currentCustomerState.ToString());
                break;
        }
        ClickToMove();
    }

    public void GetPath(Vector3 goal)
    {
        if (goal != transform.parent.position)
        {
            path = astar.Algorithm(transform.parent.position, goal);
            if (path.Count > 0)
            {
                destination = path.Pop();
                this.Goal = goal;
            }
        }
    }

    private void ClickToMove()
    {
        if (path != null)
        {
            transform.parent.position = Vector2.MoveTowards(transform.parent.position, destination, customer.SpeedMovement * Time.deltaTime);
            float distance = Vector2.Distance(destination, transform.parent.position);
            if (distance <= 0f)
            {
                if (path.Count > 1)
                {
                    destination = path.Pop();
                }
                else if(path.Count == 1)
                {
                    destination = goal;
                    path.Pop();
                }
                else
                {
                    path = null;
                }
            }
        }
    }
}
