using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerLookingDirection : MonoBehaviour {

    [Header("Looking Objects")]
    public Transform pivot;
	
	// Update is called once per frame
	void Update () {
        if(!GetComponent<scrPlayerPunchUpdate>().punching)
            LookingDirection();
	}

    void LookingDirection()
    {

        //cette fonction sert à diriger à chaque instant le regard du joueur vers la souris

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.y = 0;
        Vector3 dir = mousePos - transform.position;

        float rotY = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        pivot.rotation = Quaternion.Euler(90, rotY, 0);

    }
}
