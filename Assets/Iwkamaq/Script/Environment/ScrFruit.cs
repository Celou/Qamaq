using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrFruit : MonoBehaviour {

	public float speed;
	public float falltime;
	public GameObject parent;


	// Use this for initialization
	void Start () {
		Debug.Log ("Bonjour le fruit peut plus ou moins apapraitre");
	}

	// Update is called once per frame
	public void Update () {
	}

		public void falling()
	{
		if (gameObject.transform.position.y != -1) {
			GetComponent<Rigidbody> ().velocity = transform.up * speed;
		}
	}

	public void timing(){
		
		StartCoroutine (test ());
		} 

	IEnumerator test ()
	{
		Debug.Log ("Timer set");
		yield return new WaitForSeconds (falltime);
		Debug.Log ("Timer finish");
		GetComponent<Rigidbody> ().velocity = transform.up * 0;
		transform.gameObject.tag = "healingFruit";
		transform.gameObject.name = "Fruit juteux";
		transform.parent = null;
	}
}



 	