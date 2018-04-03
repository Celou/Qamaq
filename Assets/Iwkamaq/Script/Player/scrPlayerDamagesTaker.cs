using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerDamagesTaker : MonoBehaviour {

    [Header("Recover")]
    public float recoveringTime;
    public float currentRecoveringTime;
    public bool recovering;

    [Header("Damages Feedback")]
    public float lifeBarShakeDuration;
    public GameObject lifeBarObject;
    public GameObject damagesMask;

    void Update()
    {
        if (!GetComponent<scrPlayerPaused>().playerPaused)
            RecoverUpdate();

        if (Input.GetKeyDown(KeyCode.P) && !GetComponent<scrPlayerPaused>().playerPaused)
        {
            DealPlayerAttack(Random.Range(0f, 15f), Random.Range(500f, 1000f), new Vector3(Random.Range(-1f, 1f), 0 , Random.Range(-1f, 1f)), Random.Range(0f, 2f));
        }
    }

    void RecoverUpdate()
    {
        if (currentRecoveringTime > 0)
            currentRecoveringTime -= Time.deltaTime;

        if (currentRecoveringTime < 0)
        {
            currentRecoveringTime = 0;
            recovering = false;
        }
    }

    public void DealPlayerAttack(float damages, float knockbackForce, Vector3 knockbackDirection, float stunDuration)
    {
        if (!recovering)
        {
            GetComponent<scrPlayerLife>().TakeDamages(damages);
            GetComponent<scrPlayerKnockBack>().GetPlayerKnockback(knockbackForce, knockbackDirection);
            GetComponent<scrPlayerStun>().SetStunDuration(stunDuration);
            recovering = true;
            currentRecoveringTime = recoveringTime;

            if (GetComponent<scrPlayerPunchUpdate>().punching)
            {
                GetComponent<scrPlayerPunchUpdate>().currentDuration = -0.1f;
            }

            GetComponent<scrSlowMo>().SetSlowMo(0.2f, 0.5f);

            DamagesFeedbackStart(damages);
        }
    }

    void DamagesFeedbackStart(float damagesTaken)
    {
        damagesMask.GetComponent<scrRedMaskDamages>().baseRedDuration = 1;
        damagesMask.GetComponent<scrRedMaskDamages>().currentRedDuration = damagesMask.GetComponent<scrRedMaskDamages>().baseRedDuration;

        lifeBarObject.GetComponent<Screenshake>().shakeDuration = lifeBarShakeDuration;
        lifeBarObject.GetComponent<Screenshake>().shakeAmount = Mathf.Clamp(damagesTaken / 2, 0, 10);
    }

}
