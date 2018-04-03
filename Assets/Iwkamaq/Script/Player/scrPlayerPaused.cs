using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerPaused : MonoBehaviour {

    [Header("Basic Values")]
    public bool playerPaused;

    [Header("Paused Values")]
    Vector3 pausedRigidbody;

    private void Update()
    {
        if (scrCompetenceMenu.CompetenceMenu.inCompetenceMenu && !playerPaused)
            EnterPause();

        if (!scrCompetenceMenu.CompetenceMenu.inCompetenceMenu && playerPaused)
            ExitPause();
    }

    void EnterPause()
    {
        pausedRigidbody = GetComponent<Rigidbody>().velocity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerPaused = true;
    }

    void ExitPause()
    {
        GetComponent<Rigidbody>().velocity = pausedRigidbody;
        playerPaused = false;
    }

}
