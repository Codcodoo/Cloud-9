using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towergun : EnemyTemplate
{

    // Update is called once per frame
    void Update()
    {
        this.shotPos();
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
            StartCoroutine(Shoot(2,10,0));
        }
    }

}
