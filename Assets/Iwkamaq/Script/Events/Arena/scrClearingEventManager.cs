using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrClearingEventManager : MonoBehaviour {

	public static scrClearingEventManager ClearingEventManager;
    public bool eventing;
	public GameObject prefab;
	public bool arena;
	public Transform parent;
	//public GameObject checkpoint;
	public scrCheckpoint scrcheckpoint;

	// Use this for initialization
	void Start () 
	{
		//checkpoint = GameObject.FindGameObjectWithTag("checkpoint");
		//scrcheckpoint = checkpoint.GetComponent<scrCheckpoint>;
		ClearingEventManager = this;
		arena = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//prefab.transform.parent = transform;
		if (scrcheckpoint.playerNear == false && arena == false )
		{
			
			Instantiate (prefab, transform.position, transform.rotation).transform.parent = transform;

			arena = true;
		}

		if (scrcheckpoint.playerNear == true && arena ==true )
		{

			foreach (Transform child in parent) {
				Destroy (child.gameObject);
			}
			arena = false;
		}
	}
}
