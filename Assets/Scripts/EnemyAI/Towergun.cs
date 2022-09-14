using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towergun : MonoBehaviour
{
    public GameObject projectile;
    GameObject player;
    Vector2 playerpos;
    Vector2 dir;
    float dis = 0;
    bool loaded = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerpos = player.transform.position;
        dis = Vector2.Distance(playerpos, this.transform.position);
        dir = playerpos - new Vector2(this.transform.position.x, this.transform.position.y);
        if (dis<13)
        {
            this.GetComponent<SpriteRenderer>().color = new Color32(170,40,20,255);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
        if (loaded&&dis<10)
        {
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        GameObject shot = Instantiate(projectile, this.transform);
        Rigidbody2D shotrb = shot.GetComponent<Rigidbody2D>();
        shotrb.velocity = dir.normalized * 10;
        loaded = false;
        yield return new WaitForSeconds(2);
        loaded = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Projectile")
        {
            Destroy(this.gameObject);
        }
    }
}
