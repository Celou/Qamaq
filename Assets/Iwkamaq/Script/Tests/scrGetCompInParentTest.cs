using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrGetCompInParentTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("en utilisant la méthode propre, ça donne : " + GetComponentInParent<BoxCollider>().isTrigger);
        Debug.Log("en utilisant la méthode sale, ça donne : " + transform.parent.GetComponent<BoxCollider>().isTrigger);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
