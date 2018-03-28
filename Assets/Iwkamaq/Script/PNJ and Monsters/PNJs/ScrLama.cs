using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrLama : MonoBehaviour {

	public Transform path;
	public Transform path2;
	public float speed;
	private float step;


	// Use this for initialization
	void Awake() {
		
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			InvokeRepeating ("walk", 0f, 0.1f);
		}
	}

	void walk() {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, path.position, step);
	}
}

