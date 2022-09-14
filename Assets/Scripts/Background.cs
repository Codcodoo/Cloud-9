using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Vector2 ratiorange = new Vector2(0.1f, 0.3f);
    float ratio;
    Vector2 offset;
    GameObject player;
    Vector2 playerpos;
    public Vector2 size = new Vector2(1,3);
    float sizemult = 1;
    // Start is called before the first frame update
    void Start()
    {
        ratio = Random.Range(ratiorange.x, ratiorange.y);
        offset = this.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        sizemult = Random.Range(size.x, size.y);
        this.transform.localScale *= sizemult;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerpos = player.transform.position;
        this.transform.position =offset - playerpos*ratio;
    }
}
