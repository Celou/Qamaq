using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrTimerControler : MonoBehaviour {


	public GameObject gameobject;
	public float timer;

	void Start () {

		StartCoroutine(Spawn());
	}

	IEnumerator Spawn ()
	{
		yield return new WaitForSeconds (timer); //gère combien de temps avant que l'objet suivant (vagues) soit prête
		gameobject.SetActive(true);
	}
}