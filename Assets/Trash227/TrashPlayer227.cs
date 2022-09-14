using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPlayer227 : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    public float speed=1;
    bool onLadder = false;
    Collider2D ladderCol;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        ladderCol = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            //rb.AddForce( new Vector2(speed, rb.velocity.y));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
        if (Input.GetKey(KeyCode.UpArrow)&& onLadder)
        {
            rb.velocity = new Vector2(0, speed/2);
        }
        if (Input.GetKey(KeyCode.DownArrow) && onLadder)
        {
            rb.velocity = new Vector2(0, -speed / 2);
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), ladderCol);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name=="Ladder")
        {
            onLadder = true;
            ladderCol = collision;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Ladder")
        {
            onLadder = false;
            ladderCol = null;
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), ladderCol, true);

        }
    }
}
