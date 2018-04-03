using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrObjectStun : MonoBehaviour {

    [Header("Time")]
    public float currentStunDuration;

    [Header("Vulnerability")]
    public bool vulnerable;
    public float vulnerabilityMultiplicator;

    void Update()
    {
        StunUpdate();
    }

    void StunUpdate()
    {
        if (currentStunDuration > 0)
            currentStunDuration -= Time.deltaTime;
    }

    public void SetStunDuration(float baseStunDuration)
    {
        if (vulnerable)
        {
            currentStunDuration = baseStunDuration * vulnerabilityMultiplicator;
            return;
        }
        else
        {
            currentStunDuration = baseStunDuration;
        }
    }
    
}
