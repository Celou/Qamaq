using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMonkeyHitbox : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if(GetComponentInParent<scrMonkeyBehavior>().attack)
        {
            if(other.GetComponent<scrPlayerDamagesTaker>() != null)
            {
                other.GetComponent<scrPlayerDamagesTaker>().DealPlayerAttack(GetComponentInParent<scrMonkeyBehavior>().monkeyDamages, GetComponentInParent<scrMonkeyBehavior>().monkeyKnockBackForce, Mathf.Sign(Random.Range(-1f, 1)) * new Vector3(-GetComponentInParent<Rigidbody>().velocity.x, 0, GetComponentInParent<Rigidbody>().velocity.z), GetComponentInParent<scrMonkeyBehavior>().monkeyStunDuration);
            }
        }
    }

}
