using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrDashHitbox : MonoBehaviour {

    public float cooldownReductionIfHit;

    private void OnTriggerEnter(Collider other)
    {
        if (GetComponentInParent<scrDash>().dashing)
        {

            if (other.GetComponent<scrObjectLife>() != null)
            {
                if (other.GetComponent<scrObjectLife>().currentRecoveringTime <= 0)
                {
                    other.GetComponent<scrObjectLife>().currentLife -= GetComponentInParent<scrDash>().dashDamages;
                    other.GetComponent<scrObjectLife>().currentRecoveringTime = other.GetComponent<scrObjectLife>().recoveringTime;
                    scrCameraGlobalMovement.CameraManager.SetCameraShake("both", GetComponentInParent<scrDash>().dashShakeAmount, GetComponentInParent<scrDash>().dashShakeDuration);
                    GetComponentInParent<scrDash>().currentCooldown -= cooldownReductionIfHit;
                    Debug.Log(GetComponentInParent<scrDash>().currentCooldown);
                }                
            }

            if (other.GetComponent<scrObjectKnockback>() != null)
            {
                other.GetComponent<scrObjectKnockback>().GetKnockback(GetComponentInParent<scrDash>().dashKnockback, GetComponentInParent<scrDash>().playerBody.velocity.normalized);
            }

            if (other.GetComponent<scrObjectStun>() != null)
            {
                other.GetComponent<scrObjectStun>().SetStunDuration(GetComponentInParent<scrDash>().dashStunDuration);
            }
        }
    }
}
