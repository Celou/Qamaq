using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCheckPointManager : MonoBehaviour {

    public static scrCheckPointManager CheckpointManager;

    public GameObject activeCheckPoint;

	// Use this for initialization
	void Start () {
        CheckpointManager = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
