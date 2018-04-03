using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrSlowMo : MonoBehaviour {

    [Header("Slow Mo Variables")]
    public bool slowMoOn;
    public float currentSlowMoFactor;
    public float currentSlowMoDuration;

    private void Update()
    {
        if(!GetComponent<scrPlayerPaused>().playerPaused)
            SlowMoUpdate();
    }

    void SlowMoUpdate()
    {
        //lancement du slowMo
        if (slowMoOn && Time.timeScale == 1)
        {
            Time.timeScale = currentSlowMoFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        //atténuation du slowMo
        if (Time.timeScale < 1)
            Time.timeScale += (1f / currentSlowMoDuration) * Time.unscaledDeltaTime;

        //fin du slowMo
        if (slowMoOn && Time.timeScale > 1)
        {
            slowMoOn = false;
            Time.timeScale = 1;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }

    public void SetSlowMo(float slowMoFactor, float slowMoDuration)
    {
        currentSlowMoFactor = slowMoFactor;
        currentSlowMoDuration = slowMoDuration;
        slowMoOn = true;
    }

}
