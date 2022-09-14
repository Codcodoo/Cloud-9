using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //public GameObject player;
    //public GameObject cannon;
    //public Image startimage;
    //public Sprite startimageUnpressed;
    //public Sprite startimagePressed;
    //public Image leftimage;
    //public Image rightimage;
    //public Sprite tanksprite;
    //public Sprite dropsprite;
    public GameObject blackscreen;
    //Rigidbody2D prb;
    //bool started = false;
    //bool shotleft = false;
    //bool shotright = false;
    //float nexttime = 0.0f;
    //float period = 0.5f;
    float screenbrighttime = 255;
    //bool spacespritepressed = false;
    void Start()
    {
        blackscreen.SetActive(true);
        //prb = player.GetComponent<Rigidbody2D>();
        ////cannon.SetActive(false);
        //prb.gravityScale = 0;
    }

    void Update()
    {
        //blackscreen disapearing
        screenbrighttime -= Time.deltaTime*25;
        if (screenbrighttime > 0)//blackscreen disapearing
        {
            blackscreen.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)screenbrighttime);
        }     
        else
        {
            blackscreen.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }
        //********************************************************************
        //if (!started)
        //{
        //    if (Time.time > nexttime)//spacebar image
        //    {
        //        nexttime += period;
        //        if (spacespritepressed)
        //        {
        //            startimage.sprite = startimageUnpressed;
        //            spacespritepressed = false;
        //            leftimage.transform.Rotate(0, 0, -3);
        //            rightimage.transform.Rotate(0, 0, 3);
                    
        //        }
        //        else
        //        {
        //            startimage.sprite = startimagePressed;
        //            spacespritepressed = true;
        //            leftimage.transform.Rotate(0, 0, 3);
        //            rightimage.transform.Rotate(0, 0, -3);
        //        }
        //    }
        //}
        //*******************************************
        //if (!started&& Input.GetKey(KeyCode.Space))//start
        //{
        //    StartCoroutine(WaitStart());
        //    prb.gravityScale = 0.3f;
        //    if (!player.GetComponent<Player>().HasGun())
        //    {
        //        player.GetComponent<SpriteRenderer>().sprite = dropsprite;
        //    }
        //    startimage.gameObject.SetActive(false);
        //}
        //if (player.GetComponent<Player>().HasGun()&&!started)//start with gun
        //{
        //    player.GetComponent<SpriteRenderer>().sprite = tanksprite;
        //    player.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        //    cannon.SetActive(true);
        //    prb.velocity = new Vector2(0, 0);
        //    leftimage.gameObject.SetActive(true);
        //    rightimage.gameObject.SetActive(true);
        //}
        //if (started)//show arrows on start
        //{
        //    if (Input.GetKey(KeyCode.LeftArrow) && !shotleft)
        //    {
        //        shotleft = true;
        //        StartCoroutine(MovementText(leftimage));
        //    }
        //    if (Input.GetKey(KeyCode.RightArrow) && !shotright)
        //    {
        //        shotright = true;
        //        StartCoroutine(MovementText(rightimage));
        //    }
        //}
        if (Input.GetKey(KeyCode.R))
        {
            Restart();
        }
    }
    //public bool IsStarted()
    //{
    //    return started;
    //}
    //IEnumerator MovementText(Image image)
    //{
    //    image.color = new Color32(203, 219, 252,100);
    //    yield return new WaitForSeconds(2);
    //    image.gameObject.SetActive(false);
    //}
    //IEnumerator WaitStart()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    started = true;
    //    player.GetComponent<Player>().CheckAmmo();
    //}
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
