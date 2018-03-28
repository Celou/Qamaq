using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCheckpoint : MonoBehaviour {

    public bool playerNear;

    void Update()
    {

        if (scrCheckPointManager.CheckpointManager.activeCheckPoint == gameObject)
            GetComponent<SpriteRenderer>().color = Color.cyan;
        else
        {
            if(playerNear)
                GetComponent<SpriteRenderer>().color = new Color (0, 0.5f, 0.5f, 1);
            else
                GetComponent<SpriteRenderer>().color = Color.black;
        }

    }

    void OnTriggerEnter(Collider other)
    {

        playerNear = true;

    }

    void OnTriggerStay(Collider other)
    {

        if(Input.GetKeyDown(KeyCode.Return))
            scrCheckPointManager.CheckpointManager.activeCheckPoint = gameObject;
        
    }

    void OnTriggerExit(Collider other)
    {

        playerNear = false;

    }

}
