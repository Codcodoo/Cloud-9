using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpTemplate : MonoBehaviour
{
    public static bool[,] synergy = new bool[2, 2];
    public static bool[] powerups = new bool[2];

    protected GameObject player; // THE player

    protected Player playerScript; // THE player's script

    protected abstract int Index { get; } // index if the item

    protected virtual float Dmg { get { return 0; } } // damage

    protected virtual float LoadingTime { get { return 0; } } // the amount of time it takes to load

    protected virtual float Recoil { get { return 0; } } // recoil power

    protected virtual float ShotLife { get { return 0; } } // the time it takes the shot to get destroyed    

    protected virtual float ShotForce { get { return 0; } } // SHOTSPEEDDDDD

    protected virtual int NumOfShots { get { return 0; } } // how many shots at the same time? 

    protected virtual float TimeBetweenShots { get { return 0; } } // when you have more than one shot the time between one to another

    protected virtual float Acceleration { get { return 0; } } // accelerate n shit

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.playerScript = player.GetComponent<Player>();

        InitPowers();
        SetLoad();
        SetRecoil();
        SetShotLife();
        SetShotForce();
        SetHowManyShots();
        SetTimeBetweenShots();
        SetAcceleration();
    }

    public void InitPowers()
    {
        powerups[this.Index] = true;
        for (int i = 0; i < powerups.Length; i++)
        {
            if (powerups[i] == true && i != Index)
            {
                synergy[Index, i] = true;
                synergy[i, Index] = true;
            }
        }
    }

    public void SetLoad()
    {
        this.playerScript.loadingtime += this.LoadingTime;
    }

    public void SetRecoil()
    {
        this.playerScript.recoil += this.Recoil;
    }

    public void SetShotLife()
    {
        Drop.ShotLife += this.ShotLife;
    }

    public void SetShotForce()
    {
        this.playerScript.shotforce += this.ShotForce;
    }

    public void SetHowManyShots()
    {
        this.playerScript.NumOfShots += this.NumOfShots;
    }

    public void SetTimeBetweenShots()
    {
        this.playerScript.TimeBetweenShots += this.TimeBetweenShots;
    }

    public void SetAcceleration()
    {
        Drop.accelerate += this.Acceleration;
    }
}
