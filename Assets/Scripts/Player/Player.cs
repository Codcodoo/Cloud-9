using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject center;//rotation pivot
    public GameObject cannon;//cannon ability
    public GameObject crewpride;//end of cannon
    public GameObject hammer;//hammer ability
    public GameObject drop;//shot
    public GameObject cloudfx;//hitting clouds effect
    public GameObject oncloudfx;
    public GameObject gunshot;//gun sound
    public GameManager gm;//game manager
    public Sprite tanksprite;//tank sprite
    public Sprite tankspritefreeze;//sprite when frozen
    public Camera cam;//camera
    public GameObject hitobjsound;
    public GameObject loadedsound;
    public PhysicsMaterial2D tphm;// tank physics material
    public GameObject[] ammoind = new GameObject[3];
    Rigidbody2D rb;//player rb
    Vector2 dir;//direction vector
    public float shotforce = 10;//shot power
    public float recoil = 4;//recoil power
    public float loadingtime = 0.7f;//loading time
    public float ammomax = 2;//amount of ammo
    public float ammo;
    bool loaded = true;//loading time is over
    bool gun = false;//has gun
    bool frozen = false;//player resisting recoil
    bool incloud = false;
    bool slide = false;
    void Start()
    {
        ammo = 1;
        rb = GetComponent<Rigidbody2D>();//getting rigidbody
        rb.velocity = new Vector2(0, 0.5f);//floating up
        hammer.SetActive(false);//disable hammer by default
    }
    void FixedUpdate()
    {
        Aim();
        Hammer();
        dir = (crewpride.transform.position - center.transform.position).normalized;//direction
        Shoot(dir);
        RecoilFreeze();
        //Slide();
        if (incloud)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector2(rb.velocity.x, 5);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.velocity = new Vector2(rb.velocity.x, -5);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(5, rb.velocity.y);
            }
        }
    }

    public void Aim()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 relPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = relPos - center.transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        center.transform.eulerAngles = new Vector3(0, 0, angle);
    }
    public void Hammer()
    {
        if (Input.GetKey(KeyCode.LeftControl)) //using hammer
        {
            hammer.SetActive(true);
        }
        else //not using hammer 
        {
            hammer.SetActive(false);
        }
    }

    void Shoot(Vector2 dir)//shooting function
    {
        if ((Input.GetKey(KeyCode.Space)||Input.GetKey(KeyCode.Mouse0)) && loaded && ammo > 0 && !hammer.activeSelf)//shooting
        {
            GameObject shot = Instantiate(drop, crewpride.transform.position, Quaternion.Euler(center.transform.rotation.x, center.transform.rotation.x, center.transform.rotation.z + 45));
            Rigidbody2D shotrb = shot.GetComponent<Rigidbody2D>();
            shotrb.velocity = dir * shotforce;
            StartCoroutine(Load());
            if (!frozen)
            {
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(-dir * recoil);
            }
            else
            {
                StartCoroutine(Freeze());
            }
            Instantiate(gunshot, crewpride.transform.position, center.transform.rotation);
            if (!incloud)
            {
                ammo--;
            }
            CheckAmmo();
        }
    }

    //public void Slide()
    //{
    //    if (Input.GetKey(KeyCode.C))
    //    {
    //        slide = true;
    //    }
    //    else
    //    {
    //        slide = false;
    //    }
    //}

    public void RecoilFreeze()
    {
        Color playerColor = this.GetComponent<SpriteRenderer>().color;
        Color cannonColor = cannon.GetComponent<SpriteRenderer>().color;
        CircleCollider2D playerCollider = this.GetComponent<CircleCollider2D>();
        if (Input.GetKey(KeyCode.Z) && !frozen)
        {
            frozen = true;
            playerColor = new Color32(200, 240, 255, 255);
            cannonColor = new Color32(200, 240, 255, 255);
            playerCollider.enabled = false;
            tphm.friction = 0;
            playerCollider.enabled = true;
            slide = true;
        }
        else if (!Input.GetKey(KeyCode.Z) && frozen)
        {
            slide = false;
            frozen = false;
            playerColor = new Color32(255, 255, 255, 255);
            cannonColor = new Color32(255, 255, 255, 255);
            playerCollider.enabled = false;
            tphm.friction = 1;
            playerCollider.enabled = true;
        }
    }
   public void CheckAmmo()
    {
        
        for (int i = 0; i < ammoind.Length; i++)
        {
            if(i<ammo)
            {
                ammoind[i].SetActive(true);
            }
            else
            {
                ammoind[i].SetActive(false);
            }
        }
    }
   
    IEnumerator Load()//load gun
    {
        loaded = false;
        yield return new WaitForSeconds(loadingtime);
        StartCoroutine(Playsound(loadedsound, this.transform));
        loaded = true;
    }
    IEnumerator Playsound(GameObject sound,Transform transform)//play and delete sound
    {
        GameObject soundeffect = Instantiate(sound, transform);
        yield return new WaitForSeconds(3);
        Destroy(soundeffect);
    }
 
    IEnumerator Freeze()//switch sprites of freeze
    {
        this.GetComponent<SpriteRenderer>().sprite = tankspritefreeze;
        yield return new WaitForSeconds(loadingtime);
        this.GetComponent<SpriteRenderer>().sprite = tanksprite;
    }
    IEnumerator Drift()//coyote jump
    {
        incloud = true;
        yield return new WaitForSeconds(0.3f);
        incloud = false;

    }
 
    private void OnTriggerEnter2D(Collider2D collision)//triggering
    {

        switch (collision.transform.tag)
        {
            case "Ice":
                gun = true;//get gun
                Destroy(collision.gameObject);
                break;
            case "hurt":
                gm.Restart(); //die
                break;
            case "Platform":
                rb.gravityScale = 0;
                rb.drag = 3;
                break;
            default:
                break;
        }
        incloud = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Platform")
        {
            ammo = ammomax;
            CheckAmmo();
            rb.gravityScale = 0;
            if (slide)
            {
                rb.drag = 0;
            }
            else
            {
                rb.drag = 3;
            }
            incloud = true;
        }//drag in cloud
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Platform"){
            rb.gravityScale = 3f;
            rb.drag = 0;//drag in air
            //oncloudfx.SetActive(false);
            StartCoroutine(Drift());
        }
    }
    public bool HasGun(){return gun; }//check if player got the gun
    public Vector2 GunDir(){return dir;}
}
