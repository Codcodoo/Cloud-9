using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knocker : MonoBehaviour
{
    public GameObject projectile;
    public GameObject cannon;
    Rigidbody2D rb;
    GameObject player;
    Vector2 playerpos;
    Vector2 dir;
    float dis = 0;
    bool loaded = true;
    float angle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerpos = player.transform.position;
        dis = Vector2.Distance(playerpos, this.transform.position);
        dir = playerpos - new Vector2(this.transform.position.x, this.transform.position.y);
        if (dis < 10)//in sight
        {
            this.GetComponent<SpriteRenderer>().color = new Color32(150, 55, 100, 255);
            angle = Mathf.Atan2(playerpos.y - transform.position.y, playerpos.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            cannon.transform.rotation = Quaternion.RotateTowards(cannon.transform.rotation, targetRotation, 100 * Time.deltaTime);

        }
        else//not in sight
        {
            this.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
        if (loaded && dis < 8)//in range and can shoot
        {
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        GameObject shot = Instantiate(projectile, this.transform);
        Rigidbody2D shotrb = shot.GetComponent<Rigidbody2D>();
        shotrb.velocity = dir.normalized * 8;
        rb.velocity = -dir.normalized * 3;
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
