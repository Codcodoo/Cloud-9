using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Drop : MonoBehaviour
{
    public GameObject rainfx;
    public static float ShotLife = 1;
    public static float accelerate = 0;
    //public GameObject explode;
    //public ParticleSystem ps;
    float sizemult = 1;
    float hammerbounus = 0;
    float timer = 0;
    Vector2 velStart;
    // Start is called before the first frame update
    void Start()
    {
        velStart = this.GetComponent<Rigidbody2D>().velocity;
        StartCoroutine(Delete());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sizemult = hammerbounus + 0.5f + Mathf.Sin(Time.time * 5) / 15;
        this.transform.localScale = new Vector3(sizemult, sizemult, sizemult);
        Acceleration();
        timer += Time.deltaTime;
    }

    public void Acceleration()
    {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.Lerp(velStart, velStart * accelerate,  timer/ShotLife);
    }

    IEnumerator Delete()
    {
        // GameObject fx = Instantiate(rainfx,this.transform.position, Quaternion.Euler(player.GetComponent<Player>().GunDir().x, player.GetComponent<Player>().GunDir().y, 0));
        yield return new WaitForSeconds(ShotLife);
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
