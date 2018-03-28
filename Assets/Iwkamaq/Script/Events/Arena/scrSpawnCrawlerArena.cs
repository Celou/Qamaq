using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrSpawnCrawlerArena : MonoBehaviour {

	public GameObject gameobject;
	public float timer;
	public Vector3 here;

	void Start () {
		InvokeRepeating("SpawnCrawl", 1.0f, 5.0f);
	}
		

			void SpawnCrawl (){
		Instantiate (gameobject, transform.position, transform.rotation, transform);
        
	}
	void Update(){
	}
	}