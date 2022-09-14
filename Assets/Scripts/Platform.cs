using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position =new Vector2(transform.position.x, Mathf.Sin(Time.time)/360 + transform.position.y);
    }
}
