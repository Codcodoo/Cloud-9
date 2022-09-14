using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public GameObject rainfx;
    public float range=1;
    //public GameObject explode;
    //public ParticleSystem ps;
    GameObject player;
    float sizemult = 1;
    float hammerbounus = 0;
    Vector2 startpos;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        startpos = this.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Delete());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pos = this.transform.position;
        if (Vector2.Distance(pos,startpos)>range)
        {
            //ps.Stop();
            //ps.Play();
            //GameObject ps = Instantiate(explode, this.transform);
            //ps.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
            Destroy(gameObject);
        }
        sizemult =hammerbounus + 0.5f + Mathf.Sin(Time.time*5)/15;
        this.transform.localScale = new Vector3(sizemult, sizemult, sizemult);
    }
    IEnumerator Delete()
    {
      // GameObject fx = Instantiate(rainfx,this.transform.position, Quaternion.Euler(player.GetComponent<Player>().GunDir().x, player.GetComponent<Player>().GunDir().y, 0));
        yield return new WaitForSeconds(3);
       // Destroy(fx);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Cloud")
        {
            //Destroy(gameObject);
        }
        if (collision.collider.tag == "Hammer")
        {
            hammerbounus++;
        }
    }
}
