using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{

    public float moveSpeed = 5f;

    private Rigidbody2D rb;

    public Sprite[] directionsSprite;

    public SpriteRenderer spriteRenderer;

    Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        /*
        if (Input.GetKeyDown("up"))
        {
            spriteRenderer.sprite = directionsSprite[1];
        }
        else if (Input.GetKeyDown("down"))
        {
            spriteRenderer.sprite = directionsSprite[0];
        }
        else if (Input.GetKeyDown("left") && Input.GetKeyDown("up"))
        {
            spriteRenderer.sprite = directionsSprite[2];
        }
        */
        while(Input.GetKeyDown("left") && Input.GetKeyDown("up"))
        {
            spriteRenderer.sprite = directionsSprite[1];
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
