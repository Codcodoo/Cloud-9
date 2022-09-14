using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firstboss : MonoBehaviour
{
    public GameObject projectile;
    public Cam cam;
    Rigidbody2D rb;
    float hp = 10;
    GameObject player;
    Vector2 playerpos;
    Vector2 dir;
    float dis = 0;
    bool loaded = true;
    Vector3 offset;
    GameObject playerproj;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        playerpos = player.transform.position;
        dis = Vector2.Distance(playerpos, this.transform.position);
        dir = playerpos - new Vector2(this.transform.position.x, this.transform.position.y);
        dir.Normalize();
        
        if (dis < 6)
        {
            cam.AtBoss(true);
            playerproj = GameObject.FindGameObjectWithTag("Projectile");
            if (playerproj!=null)
            {
                if (Vector2.Distance(this.transform.position,playerproj.transform.position)<2)
                {
                    rb.AddForce(new Vector2(0, 60));
                }  
            }
            if (loaded)
            {
                loaded = false;
                StartCoroutine(Shoot());
            }
        }
        else
        {
            cam.AtBoss(false);
        }
    }
    IEnumerator Shoot()
    {
        GameObject[] shots = new GameObject[6];
        for (int i = 0; i < 6; i++)
        {
            shots[i]=Instantiate(projectile, this.transform.position,Quaternion.Euler(0,0,Random.Range(0,360)));
            Vector2 r = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.1f,-0.5f));
            shots[i].GetComponent<Rigidbody2D>().velocity =new Vector2(dir.x+r.x,dir.y+r.y);
        } 
        yield return new WaitForSeconds(3);
        loaded = true;  
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Projectile")
        {
            Destroy(collision.gameObject);
            hp--;
            if (hp==0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}