using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrGetSpriteMask : MonoBehaviour {

	void Start () {
		if(GetComponent<SpriteRenderer>() != null && GetComponent<SpriteMask>() != null)
        {
            GetComponent<SpriteMask>().sprite = GetComponent<SpriteRenderer>().sprite;
        }
	}
	
}
