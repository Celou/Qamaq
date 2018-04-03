using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrObjectStun : MonoBehaviour {

    [Header("Time")]
    public float currentStunDuration;

    [Header("Vulnerability")]
    public bool vulnerable;
    public float vulnerabilityMultiplicator;
    public bool hitWhileVulnerable;

    void Update()
    {
        StunUpdate();
    }

    void StunUpdate()
    {
        if (currentStunDuration > 0)
            currentStunDuration -= Time.deltaTime;
        if (hitWhileVulnerable && currentStunDuration < 0)
            hitWhileVulnerable = false;
    }

    public void SetStunDuration(float baseStunDuration)
    {
        if (vulnerable)
        {
            currentStunDuration = baseStunDuration * vulnerabilityMultiplicator;
            hitWhileVulnerable = true;
            return;
        }
        else
        {
            currentStunDuration = baseStunDuration;
        }
    }
    
}
