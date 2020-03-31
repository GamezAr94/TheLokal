using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{
    public float movementSpeed;

    //Rigidbody2D rbody;
    private Vector2 targetPosition;
    private Camera myCamera;
    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;
        //rbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CalculateTargetPosition();
            Debug.Log(targetPosition);
        }
        MoveToTarger();
    }

    private void MoveToTarger()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
    }

    private void CalculateTargetPosition()
    {
        targetPosition = myCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        Vector2 currentPosition = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPosition + movement * Time.fixedDeltaTime;
        rbody.MovePosition(newPos);
        */
    }
}
