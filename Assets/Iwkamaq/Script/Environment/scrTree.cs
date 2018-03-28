using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrTree : MonoBehaviour {

    public GameObject fruit;
	public ScrFruit fs;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter (Collider other)
    {
		if (other.tag == "playerPunch")
		{
			GameObject fruit = GameObject.FindWithTag ("Fruit");
			if (fruit != null) {
				fs = GetComponentInChildren<ScrFruit> ();
					fs.falling ();
					fs.timing ();
    }

}
}
}
