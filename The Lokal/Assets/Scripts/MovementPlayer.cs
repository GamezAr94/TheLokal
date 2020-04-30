using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{

    public float moveSpeed = 5f;

    private Rigidbody2D rb;

    public SpriteRenderer spriteRenderer;

    Vector2 movement;

    float moveH, moveV;

    Drinks reachingDrink;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    /*
    private void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            movement = new Vector2(-moveSpeed, 0);
        }
        else if (Input.GetKeyDown("w"))
        {
            movement = new Vector2(0,moveSpeed);
        }
        else if (Input.GetKeyDown("d"))
        {
            movement = new Vector2(moveSpeed, 0);
        }
        else if (Input.GetKeyDown("s"))
        {
            movement = new Vector2(0, -moveSpeed);
        }
    }
    */
    // Update is called once per frame
    void FixedUpdate()
    {
        
        moveH = Input.GetAxis("Horizontal") * moveSpeed;
        moveV = Input.GetAxis("Vertical") * moveSpeed;
        rb.velocity = new Vector2(moveH, moveV);

        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position + new Vector3(-.5f,.5f), new Vector3(0, 0)); 

        //If something was hit.
        if (hit.collider != null && hit.collider.CompareTag("Drink"))
        {
            reachingDrink = hit.collider.GetComponent<Drinks>();
            reachingDrink.isGrabable = true;
            Debug.Log(hit.collider.ToString());
        }
        else if (!hit.collider.CompareTag("Drink") && reachingDrink != null)
        {
            reachingDrink.isGrabable = false;
            reachingDrink = null;
        }

        Debug.DrawLine(this.gameObject.transform.position, this.gameObject.transform.position + new Vector3(-.5f, .5f));
        /*
        rb.velocity = movement;*/
        if (rb.velocity.normalized.x >= 1)
        {
            rb.SetRotation(90);
        }
        if (rb.velocity.normalized.x <= -1)
        {
            rb.SetRotation(-90);
        }
        if (rb.velocity.normalized.y >= 1)
        {
            rb.SetRotation(180);
        }
        if (rb.velocity.normalized.y <= -1)
        {
            rb.SetRotation(0);
        }

        if (rb.velocity.normalized.x > 0 && rb.velocity.normalized.y > 0)
        {
            rb.SetRotation(135);
        }
        if (rb.velocity.normalized.x < 0 && rb.velocity.normalized.y > 0)
        {
            rb.SetRotation(225);
        }
        if (rb.velocity.normalized.x < 0 && rb.velocity.normalized.y < 0)
        {
            rb.SetRotation(315);
        }
        if (rb.velocity.normalized.x > 0 && rb.velocity.normalized.y < 0)
        {
            rb.SetRotation(45);
        }

        /*
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
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
        */
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
