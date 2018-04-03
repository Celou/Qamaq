using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCompetenceUnlocker : MonoBehaviour {

    [Header("Locks")]
    public bool lightningUnlocked;
    public bool dashUnlocked;
    public bool protectionSphereUnlocked;
    public bool boostUnlocked;
    public bool golemUnlocked;
    public bool timeControlUpdate;

    [Header("Costs")]
    public int firstCompCost;
    public int secondCompCost;
    public int thirdCompCost;
    public int fourthCompCost;
    public int fifthCompCost;
    public int sixthCompCost;
    int[] Costs = new int[6];
    public int currentCompCost;

    private void Start()
    {
        Costs[0] = firstCompCost;
        Costs[1] = secondCompCost;
        Costs[2] = thirdCompCost;
        Costs[3] = fourthCompCost;
        Costs[4] = fifthCompCost;
        Costs[5] = sixthCompCost;
        currentCompCost = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            UnlockProtectionSphere();

        if (Input.GetKeyDown(KeyCode.L))
            UnlockLightning();

        if (Input.GetKeyDown(KeyCode.M))
            UnlockDash();
    }

    public void UnlockLightning()
    {
        if (!GetComponentInParent<scrOffensiveCompetence>().enabled && scrPlayerExperience.PlayerExperience.currentPlayerExperience >= Costs[currentCompCost])
        {
            GetComponentInParent<scrOffensiveCompetence>().enabled = true;

            scrPlayerExperience.PlayerExperience.currentPlayerExperience -= Costs[currentCompCost];
            currentCompCost += 1;
            //rendre le bouton inintéractible
            //jouer le son de déblocage
        }
    }

    public void UnlockProtectionSphere()
    {
        if (!GetComponentInParent<scrPlayerExperience>().GetComponentInChildren<scrDefensiveCompetence>().enabled && scrPlayerExperience.PlayerExperience.currentPlayerExperience >= Costs[currentCompCost])
        {
            GetComponentInParent<scrPlayerExperience>().GetComponentInChildren<scrDefensiveCompetence>().enabled = true;

            scrPlayerExperience.PlayerExperience.currentPlayerExperience -= Costs[currentCompCost];
            currentCompCost += 1;
            //rendre le bouton inintéractible
            //jouer le son de déblocage
        }
    }

    public void UnlockDash()
    {
        if (!GetComponentInParent<scrDash>().enabled && scrPlayerExperience.PlayerExperience.currentPlayerExperience >= Costs[currentCompCost])
        {
            GetComponentInParent<scrDash>().enabled = true;

            scrPlayerExperience.PlayerExperience.currentPlayerExperience -= Costs[currentCompCost];
            currentCompCost += 1;
            //rendre le bouton inintéractible
            //jouer le son de déblocage
        }
    }
}
