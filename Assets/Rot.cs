﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rot : MonoBehaviour
{
    float a = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //a += 1.0f;
        transform.Rotate(new Vector3(0.0f, 0.0f, a));
    }
}
