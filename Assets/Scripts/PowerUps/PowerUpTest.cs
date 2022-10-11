using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PowerUpTest : PowerUpTemplate
{
    protected override int Index
    {
        get { return 0; }
    }

    protected override float Dmg
    {
        get { return 3; }
    }

    protected override float LoadingTime
    {
        get { return 0.3f; }
    }

    protected override float Recoil
    {
        get { return 1500; }
    }

    protected override float ShotLife
    {
        get { return 0f; }
    }

    protected override float ShotForce
    {
        get { return -6; }
    }

    protected override int NumOfShots
    {
        get { return 0; }
    }

    protected override float TimeBetweenShots
    {
        get { return 0.05f; }
    }

    protected override float Acceleration
    {
        get { return -2f; }
    }

    //protected override void Special()
    //{
    //    Vector2 lightEndPos;
    //    RaycastHit2D hit = Physics2D.Raycast(player.transform.position, playerScript.dir * 15, 50);
    //    if (hit)
    //    {
    //        lightEndPos = hit.point;
    //    }
    //    else
    //    {
    //        lightEndPos = playerScript.dir * 15;
    //    }

    //    Debug.DrawRay(player.transform.position, playerScript.dir * 10, Color.red, 3, true);

    //    GameObject myLine = new GameObject();
    //    Vector3 lightStartPos = player.transform.position;
    //    myLine.transform.position = lightStartPos;
    //    myLine.AddComponent<LineRenderer>();
    //    LineRenderer lr = myLine.GetComponent<LineRenderer>();
    //    lr.material = new Material(Shader.Find("Sprites/Default"));
    //    //lr.SetColors(Color.yellow, Color.white);
    //    lr.startColor = Color.yellow;
    //    lr.endColor = Color.white;
    //    //lr.SetWidth(0.1f, 0.1f);
    //    lr.startWidth = 0.2f;
    //    lr.endWidth = 0.1f;
    //    lr.SetPosition(0, lightStartPos);
    //    lr.SetPosition(1, lightEndPos);
    //    GameObject.Destroy(myLine, 0.5f);
    //}

}
