using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCameraEventMovement : MonoBehaviour {

    [Header("Positions")]
    public bool isEventing;
    public Vector3 currentEventPosition;

	// Update is called once per frame
	void Update () {
        if (isEventing)
        {
            GetComponent<scrCameraGlobalMovement>().currentGoalPosition = currentEventPosition;
        }		
	}
}
