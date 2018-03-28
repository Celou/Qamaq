using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrLightningManager : MonoBehaviour {

    static scrLightningManager LightningManager;

    public GameObject Player;
    
    [Header ("Basic Lightning")]
    public GameObject[] PlayerLightningArray;
    public GameObject FallBackPlayerLightning;
    

	// Use this for initialization
	void Start () {

        LightningManager = this;

	}
	
	// Update is called once per frame
	void Update () {

        for (int compteurLightning = 0; compteurLightning < LightningManager.PlayerLightningArray.Length; compteurLightning++)
        {

            if (Vector3.Distance(LightningManager.Player.GetComponent<scrPlayer>().transform.position, LightningManager.PlayerLightningArray[compteurLightning].GetComponent<scrPlayerLightning>().transform.position) > 20)
                PlayerLightningArray[compteurLightning].GetComponent<scrPlayerLightning>().isDestroyed = true;


        }

    }

    public static GameObject GetNewLightning(Vector3 newPosition, Quaternion newRotation)
    {

        GameObject availableLightning = null;

        for (int compteurLightning = 0; compteurLightning < LightningManager.PlayerLightningArray.Length; compteurLightning++) {

            bool thisLightingIsAvailable = false;

            if (Vector3.Distance(LightningManager.Player.GetComponent<scrPlayer>().transform.position, LightningManager.PlayerLightningArray[compteurLightning].GetComponent<scrPlayerLightning>().transform.position) > 15)
                thisLightingIsAvailable = true;
            else
                thisLightingIsAvailable = false;

            if (thisLightingIsAvailable)
            {
                
                availableLightning = LightningManager.PlayerLightningArray[compteurLightning];
                LightningManager.PlayerLightningArray[compteurLightning].GetComponent<scrPlayerLightning>().enabled = true;
                availableLightning.GetComponent<scrPlayerLightning>().isDestroyed = false;
                break;
                

            }

        }

        if (availableLightning == null)
        {

            availableLightning = Instantiate(LightningManager.FallBackPlayerLightning, newPosition, newRotation);
            Debug.Log(LightningManager.PlayerLightningArray.Length);

        }

        availableLightning.transform.position = newPosition;
        availableLightning.transform.rotation = newRotation;

        return availableLightning;

    }

}
