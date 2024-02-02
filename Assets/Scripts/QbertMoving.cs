using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QbertMoving : MonoBehaviour
{
    public float speed = 5;
    public Transform movePointW, movePointA, movePointS, movePointD;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += movePointW.position;
        }
    }
}
