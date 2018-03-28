﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrLittleRock : MonoBehaviour {

    public bool throwed;
    public float speed;
    public float lifeTime;
    public float currentLifeTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (currentLifeTime > 0)
            currentLifeTime -= Time.deltaTime;
        
        if (currentLifeTime < 0)
        {

            transform.position += new Vector3(0,20,0);
            enabled = false;

        }

        if (currentLifeTime < 0.2f && throwed == true)
            transform.Translate(new Vector3(0,0,speed * (-0.02f/(currentLifeTime+0.01f)) * Time.deltaTime), Space.World);


        transform.Translate(Vector3.up * Time.deltaTime * speed);


	}
}
