using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Astar astar;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private LayerMask ground;

    private Stack<Vector3> path;

    private Vector3 destination;

    private Vector3 goal;

    [SerializeField]
    private Customer customer;


    public Vector3 Goal { get => goal; set => goal = value; }

    public void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,Mathf.Infinity,ground);
            if (hit.collider!= null)
            {
                Vector3 goalPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                GetPath(goalPosition);
            }
            
        }
        if(customer.IsWalking && path == null)
        {
            GetPath(customer.GetNextStop());
        }
        /*
        if(customer.GetCurrentState() == 0 && path == null && transform.parent.position != PositionsObjects.Cashier) { 
            //ir al cajero
            GetPath(PositionsObjects.Cashier);
        }if (customer.GetCurrentState() == 1)
        {
            Debug.Log("Ordering");
        }if(customer.GetCurrentState() == 2 && path == null && transform.parent.position != customer.GetNextStop())
        {
            GetPath(customer.GetNextStop());
        }if(customer.GetCurrentState() == 3)
        {
            Debug.Log("Waiting");
        }
        */
        /*
            else if (customer.HasOrdered && path == null)
            {
                //encontrar una silla
            }*/
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
                if (path.Count > 0)
                {
                    destination = path.Pop();
                }
                else
                {
                    path = null;
                }
            }
        }
    }
}
