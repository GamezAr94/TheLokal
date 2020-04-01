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

    private Stack<Vector3> path;

    private Vector3 destination;

    private Vector3 goal;

    [SerializeField]
    private Customer customer;


    public Vector3 Goal { get => goal; set => goal = value; }
    private void Awake()
    {
        mainCamera = Camera.main;

    }
    public void FixedUpdate()
    {
        if(customer.IsWalking && path == null)
        {
            GetPath(customer.GetNextStop());
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
