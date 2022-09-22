using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knocker : EnemyTemplate
{
    public GameObject cannon;
    float angle;

    // Update is called once per frame
    void Update()
    {
        this.shotPos();
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
            StartCoroutine(Shoot(2,8,3));
        }
    }
}
