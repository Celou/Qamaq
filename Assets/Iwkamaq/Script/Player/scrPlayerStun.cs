using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerStun : MonoBehaviour {

    [Header("StunValues")]
    public bool stuned;

    [Header("Time")]
    public float currentStunDuration;

    void Update()
    {
        if(!GetComponent<scrPlayerPaused>().playerPaused)
            StunUpdate();
    }

    void StunUpdate()
    {
        if (currentStunDuration > 0)
            currentStunDuration -= Time.deltaTime;

        if(currentStunDuration < 0)
        {
            currentStunDuration = 0;
            stuned = false;
        }
    }

    public void SetStunDuration(float baseStunDuration)
    {
        stuned = true;
        currentStunDuration = baseStunDuration;        
    }
}
