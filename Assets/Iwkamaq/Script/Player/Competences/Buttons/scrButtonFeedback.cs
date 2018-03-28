using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrButtonFeedback : MonoBehaviour {

    public string compName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(compName == "yamtaLightning")
        {

            if (scrExperienceManager.ExperienceManager.yamtaLightningUnclocked)
            {

            }

        }

	}
}
