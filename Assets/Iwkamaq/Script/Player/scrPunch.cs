using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPunch : MonoBehaviour {

    public float punchKnockbackForce;
    public float punchStunDuration;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<scrPlayer>() == null)
        {
            if (other.GetComponent<scrObjectLife>() != null)
            {
                if (other.GetComponent<scrObjectLife>().currentRecoveringTime <= 0)
                {
                    other.GetComponent<scrObjectLife>().currentLife -= GetPlayerPunchDamages();
                    other.GetComponent<scrObjectLife>().currentRecoveringTime = other.GetComponent<scrObjectLife>().recoveringTime;
                }
            }

            if (other.GetComponent<scrObjectKnockback>() != null)
            {
                other.GetComponent<scrObjectKnockback>().GetKnockback(GetPlayerKnockbackForce(), transform.rotation * Vector3.up);
            }

            if (other.GetComponent<scrObjectStun>() != null)
                other.GetComponent<scrObjectStun>().SetStunDuration(GetPlayerStunDuration());
        }
    }

    float GetPlayerPunchDamages()
    {
        if (GetComponentInParent<scrPlayerPunchUpdate>().currentPunchCombo > 0)
            return GetComponentInParent<scrPlayer>().currentPlayerDamage;
        if (GetComponentInParent<scrPlayerPunchUpdate>().currentPunchCombo == 0)
            return GetComponentInParent<scrPlayer>().currentPlayerDamage * GetComponentInParent<scrPlayerPunchUpdate>().finalPunchDamagesMultiplicator;
        
        return 0;
    }

    float GetPlayerKnockbackForce()
    {
        if (GetComponentInParent<scrPlayerPunchUpdate>().currentPunchCombo > 0)
            return punchKnockbackForce;
        if (GetComponentInParent<scrPlayerPunchUpdate>().currentPunchCombo == 0)
            return punchKnockbackForce * GetComponentInParent<scrPlayerPunchUpdate>().finalPunchDamagesMultiplicator;
        
        return 0;
    }

    float GetPlayerStunDuration()
    {
        if (GetComponentInParent<scrPlayerPunchUpdate>().currentPunchCombo > 0)
            return punchStunDuration;
        if (GetComponentInParent<scrPlayerPunchUpdate>().currentPunchCombo == 0)
            return punchStunDuration * GetComponentInParent<scrPlayerPunchUpdate>().finalPunchDamagesMultiplicator * 5;

        return 0;
    }
}
