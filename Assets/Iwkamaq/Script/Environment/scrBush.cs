using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBush : MonoBehaviour {

    public GameObject HealingFruit;

	void Start () {
		
	}
	
	void Update () {
		
	}

    void OnTriggerEnter (Collider Other)
    {

        if (Other.tag == "playerPunch")
        {
            
            Instantiate(HealingFruit, transform.position, transform.rotation);
            Destroy(gameObject);

        }

    }

}
