using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTest2 : PowerUpTemplate
{
    // Start is called before the first frame update

    protected override int Index
    {
        get { return 1; }
    }

    protected override int NumOfShots
    {
        get { return 1; }
    }
}
