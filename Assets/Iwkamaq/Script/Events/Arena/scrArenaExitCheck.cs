using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrArenaExitCheck : MonoBehaviour {

	public GameObject ronce;
	public GameObject Wave;
    public bool publicDestroyed;
    bool destroyed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(publicDestroyed && !destroyed)
        {
            Debug.Log("destruction");
            Debug.Log("destruction");
            destroyed = true;
            StartCoroutine(Destroying());
        }
		
	}

	void OnTriggerEnter (Collider other){

		if (other.tag == "Player") {
			ronce.SetActive (true);
			Wave.SetActive (true);
            scrClearingEventManager.ClearingEventManager.eventing = true;
		}
    }

    IEnumerator Destroying()
    {
        yield return new WaitForSeconds(2);

        scrCamera.CameraManager.GoalPosition = ronce.transform.position;

        yield return new WaitForSeconds(1);

        Destroy(ronce.gameObject);

        yield return new WaitForSeconds(1);

        scrCamera.CameraManager.GoalPosition = scrCamera.CameraManager.Player.transform.position;

        yield return new WaitForSeconds(1);

        scrCamera.CameraManager.posX = 0;

        scrCamera.CameraManager.posZ = 0;

        scrCamera.CameraManager.eventing = false;

        scrClearingEventManager.ClearingEventManager.eventing = false;
    }
}
