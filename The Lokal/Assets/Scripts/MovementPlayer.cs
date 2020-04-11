using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{

    public float moveSpeed = 5f;

    private Rigidbody2D rb;

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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            rb.SetRotation(90);
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            rb.SetRotation(-90);
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            rb.SetRotation(180);
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            rb.SetRotation(0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Tables"))
        {
            spriteRenderer.sortingOrder = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals("Entrance"))
        {
            spriteRenderer.sortingOrder = spriteRenderer.sortingOrder > 2 ? 1 : 15;
        }
        if (collision.tag.Equals("Tables"))
        {
            spriteRenderer.sortingOrder = 15;
        }
    }
}
