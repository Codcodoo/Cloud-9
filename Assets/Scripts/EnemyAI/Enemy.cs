using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyTemplate
{
    public GameObject cannon;
    bool insightmax = false;
    bool insightmin = false;
    bool inrange = false;
    float angle;


    // Update is called once per frame
    void FixedUpdate()
    {
        this.shotPos();
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
        if (this.loaded&&inrange)
        {
            StartCoroutine(Shoot(3, 10, 2));
        }
        if (dis>12)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

}
