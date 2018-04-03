using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCompetenceMenu : MonoBehaviour {

    public static scrCompetenceMenu CompetenceMenu;

    [Header("Public Values")]
    public bool inCompetenceMenu;

    [Header("Stocked Values")]
    Vector3 pausedPlayerRigidbody;

    private void Start()
    {
        CompetenceMenu = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!inCompetenceMenu)
                EnterCompetenceMenu();
            else
                ExitCompetenceMenu();
        }
    }

    void EnterCompetenceMenu()
    {
        inCompetenceMenu = true;
    }

    void ExitCompetenceMenu()
    {
        inCompetenceMenu = false;
    }
}
