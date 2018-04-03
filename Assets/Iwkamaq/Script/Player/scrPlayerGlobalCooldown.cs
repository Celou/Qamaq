using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerGlobalCooldown : MonoBehaviour {

    [Header("Cooldown")]
    public float currentGlobalCooldown;

    private void Update()
    {
        if(!GetComponent<scrPlayerPaused>().playerPaused)
            GlobalCooldownUpdate();
    }

    void GlobalCooldownUpdate()
    {
        if (currentGlobalCooldown > 0)
            currentGlobalCooldown -= Time.deltaTime;

        if (currentGlobalCooldown < 0)
            currentGlobalCooldown = 0;
    }

}
