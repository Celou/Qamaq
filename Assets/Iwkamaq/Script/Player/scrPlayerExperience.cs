using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerExperience : MonoBehaviour {

    public static scrPlayerExperience PlayerExperience;

    [Header("Player Experience")]
    public int currentPlayerExperience;
    float totalPlayerExperience;


    private void Start()
    {
        PlayerExperience = this;
    }

    private void Update()
    {
        Debug.Log("Experience actuelle : " + currentPlayerExperience);

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            GainExperience(50);

        if(Input.GetKeyDown(KeyCode.KeypadMinus))
            GainExperience(-50);
    }

    public void GainExperience(int experienceAmount)
    {
        currentPlayerExperience += experienceAmount;
        totalPlayerExperience += experienceAmount;
    }

}
