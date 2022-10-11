using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplate : MonoBehaviour
{
    public GameObject projectile; // enemy projectile
    protected Rigidbody2D rb; // enemy rigidbody2d
    protected GameObject player; // THE player

    protected bool loaded = true; // if you can shoot or not

    protected Vector2 playerpos; // player position
    protected Vector2 dir; // based on the player's pos the enemy shoot to that direction
    protected float dis = 0; // distance from the player
    
    public virtual void collision() { } // a template collision func
    public virtual void shootTemp() { } // a template shoot func

    void Start()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.player = GameObject.FindGameObjectWithTag("Player");

    }
    public IEnumerator Shoot(float time, float shotVel, float rbVel)
    {
        GameObject shot = Instantiate(projectile, this.transform);
        Rigidbody2D shotrb = shot.GetComponent<Rigidbody2D>();
        shotrb.velocity = dir.normalized * shotVel;
        this.rb.velocity = -dir.normalized * rbVel;
        shootTemp();
        loaded = false;
        yield return new WaitForSeconds(time);
        loaded = true;
    }

    public void shotPos()
    {
        this.playerpos = player.transform.position;
        this.dis = Vector2.Distance(playerpos, this.transform.position);
        this.dir = playerpos - new Vector2(this.transform.position.x, this.transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Projectile")
        {
            Destroy(this.gameObject);
        }

        this.collision();
    }
}
