using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cam : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    private Transform camerapos;
    public float offsety = -1f;
    public Vector3 offset;
    public float speedx = 0;
    public float speedy=0;
    public Camera cam;
    bool atboss = false;
    void Start()
    {
        offset = new Vector3(0, offsety, 10);
        camerapos = this.transform;
        rb = player.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!atboss)
        {
            if (Input.GetKey(KeyCode.DownArrow) && offsety < 1)//look down
            {
                offsety += Time.deltaTime * 3;
            }//return up
            else
            {
                if (offsety > -1 && !(Input.GetKey(KeyCode.DownArrow)))
                {
                    offsety -= Time.deltaTime * 5;
                }
            }
            // x offset by movement
            if (rb.velocity.x > 0.3)//right
            {
                if (speedx < rb.velocity.x && speedx < 2)
                {
                    speedx += Time.deltaTime / 2;
                }
            }
            else if (rb.velocity.x < -0.3)
            {
                if (speedx > rb.velocity.x && speedx > -2)
                {
                    speedx -= Time.deltaTime / 2;
                }
            }
            if ((rb.velocity.x < 0.1 && rb.velocity.x >= 0) || (rb.velocity.x > -0.1 && rb.velocity.x <= 0))
            {
                if (speedx > 0)
                {
                    speedx -= Time.deltaTime;
                }
                if (speedx < 0)
                {
                    speedx += Time.deltaTime;
                }
                if (speedx < 0.01 && speedx > -0.01)
                {
                    speedx = 0;
                }
            }
            // y offset by movement
            if (rb.velocity.y > 0.3)//up
            {
                if (speedy < rb.velocity.y && speedy < 0.5)
                {
                    speedy += Time.deltaTime / 2;
                }
            }
            else if (rb.velocity.y < -0.3)
            {
                if (speedy > rb.velocity.y && speedy > -3)
                {
                    speedy -= Time.deltaTime / 2;
                }
            }
            if ((rb.velocity.y < 0.1 && rb.velocity.y >= 0) || (rb.velocity.y > -0.1 && rb.velocity.y <= 0))
            {
                if (speedy > 0)
                {
                    speedy -= Time.deltaTime;
                }
                if (speedy < 0)
                {
                    speedy += Time.deltaTime;
                }
                if (speedy < 0.01 && speedy > -0.01)
                {
                    speedy = 0;
                }
            }

            offset.x = -speedx;
            offset.y = -speedy + offsety;
            camerapos.position = player.transform.position - offset;
            //cam.orthographicSize = 2;
        }
        else
        {
            Gettoboss();
        }
    }
    void Gettoboss()
    {
        camerapos.position = new Vector3(0, 8, -10);
        cam.orthographicSize = 3.5f;
    }
    public void AtBoss(bool isatboss)
    {
        atboss = isatboss;
    }
}
