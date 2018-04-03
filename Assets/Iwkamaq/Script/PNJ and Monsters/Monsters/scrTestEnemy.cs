using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrTestEnemy : MonoBehaviour {

    public float baseDamages;
    public Material normalMaterial;
    public Material stunMaterial;

    void Update()
    {
        StunCheck();
    }

    void StunCheck()
    {
        if (GetComponentInChildren<scrObjectStun>().currentStunDuration > 0)
            GetComponent<Renderer>().material = stunMaterial;
        else
            GetComponent<Renderer>().material = normalMaterial;
    }

}
