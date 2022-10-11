using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergies : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;
    
    private void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    public static void InitSynergy(int index)
    {
    }

    private void Index0And1()
    {
        Vector2 lightEndPos;
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, playerScript.dir * 15, 50);
        if (hit)
        {
            lightEndPos = hit.point;
        }
        else
        {
            lightEndPos = playerScript.dir * 15;
        }

        Debug.DrawRay(player.transform.position, playerScript.dir * 10, Color.red, 3, true);

        GameObject myLine = new GameObject();
        Vector3 lightStartPos = player.transform.position;
        myLine.transform.position = lightStartPos;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        //lr.SetColors(Color.yellow, Color.white);
        lr.startColor = Color.yellow;
        lr.endColor = Color.white;
        //lr.SetWidth(0.1f, 0.1f);
        lr.startWidth = 0.2f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, lightStartPos);
        lr.SetPosition(1, lightEndPos);
        GameObject.Destroy(myLine, 0.5f);
    }
}
