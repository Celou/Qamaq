using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEventManager : MonoBehaviour {

    public static scrEventManager EventManager;

    [Header("Player")]
    public GameObject Player;

    [Header("Camera")]
    public GameObject mainCamera;

    [Header("FirstCine")]
    public Vector3 FirstFocusPoint;
    public Vector3 SecondFocusPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!mainCamera.GetComponent<scrCamera>().eventing && Input.GetKeyDown(KeyCode.P))
            StartCoroutine(EventOne());
	}

    IEnumerator EventOne()
    {
        Debug.Log("debut ciné");

        mainCamera.GetComponent<scrCamera>().eventing = true;

        mainCamera.GetComponent<scrCamera>().GoalPosition = FirstFocusPoint;

        yield return new WaitForSeconds(1);

        mainCamera.GetComponent<scrCamera>().GoalPosition = SecondFocusPoint;

        yield return new WaitForSeconds(1);

        mainCamera.GetComponent<scrCamera>().GoalPosition = Player.transform.position;

        yield return new WaitForSeconds(1);

        mainCamera.GetComponent<scrCamera>().posX = 0;

        mainCamera.GetComponent<scrCamera>().posZ = 0;

        Debug.Log("fin ciné");

        mainCamera.GetComponent<scrCamera>().eventing = false;

    }
}
