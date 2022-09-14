using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectile;
    public GameObject cannon;
    Rigidbody2D rb;
    GameObject player;
    Vector2 playerpos;
    Vector2 dir;
    float dis = 0;
    bool loaded = true;
    bool insightmax = false;
    bool insightmin = false;
    bool inrange = false;
    float angle;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerpos = player.transform.position;
        dis = Vector2.Distance(playerpos, this.transform.position);
        dir = playerpos - new Vector2 (this.transform.position.x,this.transform.position.y);
        if (dis<12){ insightmax = true; }//in  sight max
        else { insightmax = false; }//not in sight max
        if (dis > 8) { insightmin = true; }//in sight min
        else { insightmin = false; }//not in sight min
        if (dis<4){ inrange = true;}//in range
        else { inrange = false;}//not in range

        if (insightmax&&insightmin)
        {
            rb.AddForce(dir.normalized*8);
            this.GetComponent<SpriteRenderer>().color = new Color32(150, 55, 100, 255);
            angle = Mathf.Atan2(playerpos.y - transform.position.y, playerpos.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            cannon.transform.rotation = Quaternion.RotateTowards(cannon.transform.rotation, targetRotation, 100 * Time.deltaTime);
        }
        if (loaded&&inrange)
        {
            StartCoroutine(Shoot());
        }
        if (dis>12)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
    IEnumerator Shoot()
    {
        GameObject shot = Instantiate(projectile, this.transform);
        Rigidbody2D shotrb = shot.GetComponent<Rigidbody2D>();
        shotrb.velocity = dir.normalized*10;
        rb.velocity = -dir.normalized*2;
        loaded = false;
        yield return new WaitForSeconds(3);
        loaded = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag== "Projectile")
        {
            Destroy(this.gameObject);
        }
    }
}
