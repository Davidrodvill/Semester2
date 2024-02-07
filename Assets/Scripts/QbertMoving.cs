using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QbertMoving : MonoBehaviour
{
    public float speed = 5;
    public GameObject movePointW, movePointA, movePointS, movePointD;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += movePointW.transform.position;
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position -= movePointS.transform.position;
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.position += movePointD.transform.position;
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.position -= movePointA.transform.position;
        }
    }
}
