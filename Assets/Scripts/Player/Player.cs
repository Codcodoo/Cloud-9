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
    public GameObject oncloudfx;//constant effect on cloud
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
    public float shotforce;//shot power
    public float recoil;//recoil power
    public float loadingtime;//loading time
    public float ammomax;//amount of ammo
    public float ammo;
    public float gravityOutCloud;
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
    void Update()
    {  
        Aim();//aiming the cannon by rotating the center
        Hammer();//using the hammer
        dir = (crewpride.transform.position - center.transform.position).normalized;//direction
        Shoot(dir);//shooting
        RecoilFreeze();//freezing
        //Slide();
        if (incloud)//wasd movement that we should remove
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
        //Vector3 mousePos = Input.mousePosition;//position of the mouse
        //Vector3 relPos = Camera.main.ScreenToWorldPoint(mousePos);//position of the mouse rellative to camera
        //Vector2 direction = relPos - center.transform.position;//aim direction
        //float angle = Vector2.SignedAngle(Vector2.up, direction);//angle of rotation changes accordingly
        //center.transform.eulerAngles = new Vector3(0, 0, angle);//rotating the center

        //Arrow rotation that we might want to use for reasons
        if (Input.GetKey(KeyCode.RightArrow))
        {
            center.transform.Rotate(0, 0, -400 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            center.transform.Rotate(0, 0, 400 * Time.deltaTime);
        }
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
        if (Input.GetMouseButton(0)||Input.GetKey(KeyCode.Space))
        {
            slide = true;//sliding while in cloud
            
            if (Input.GetKeyDown(KeyCode.Space)&&loaded && ammo > 0 && !hammer.activeSelf)//shooting
            {
                GameObject shot = Instantiate(drop, crewpride.transform.position, Quaternion.Euler(center.transform.rotation.x, center.transform.rotation.x, center.transform.rotation.z + 45));
                Rigidbody2D shotrb = shot.GetComponent<Rigidbody2D>();
                shotrb.velocity = dir * shotforce;
                StartCoroutine(Load());
                if (!frozen)
                {
                    rb.velocity = new Vector2(0, 0);//shot reset
                    rb.AddForce(-dir * recoil);//shot moving
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
        else
        {
            slide = false;
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
            //case "Platform":
            //    rb.gravityScale = 0;
            //    rb.drag = 3;
            //    break;
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
            rb.gravityScale = gravityOutCloud;
            rb.drag = 0;//drag in air
            //oncloudfx.SetActive(false);
            StartCoroutine(Drift());
        }
    }
    public bool HasGun(){return gun; }//check if player got the gun
    public Vector2 GunDir(){return dir;}
}
