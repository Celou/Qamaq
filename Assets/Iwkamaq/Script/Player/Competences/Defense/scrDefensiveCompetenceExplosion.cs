using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrDefensiveCompetenceExplosion : MonoBehaviour {

    public float paybackDamages;
    public float knockbackForce;
    public float stunDuration;
    public float maxRadius;
    public float expandSpeed;

    private void Start()
    {
        GetComponent<SphereCollider>().radius = 0;
    }

    void Update () {
        if (GetComponent<SphereCollider>().radius < maxRadius && !scrCompetenceMenu.CompetenceMenu.inCompetenceMenu)
            GetComponent<SphereCollider>().radius += expandSpeed * Time.deltaTime;
        else
            Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<scrObjectKnockback>() != null)
        {
            other.GetComponent<scrObjectKnockback>().GetKnockback(knockbackForce, (other.GetComponent<Transform>().position - transform.position).normalized);
        }

        if (other.GetComponent<scrObjectLife>() != null)
        {
            if(other.GetComponent<scrObjectLife>().currentRecoveringTime == 0)
            {
                other.GetComponent<scrObjectLife>().currentLife -= paybackDamages;
                other.GetComponent<scrObjectLife>().currentRecoveringTime = other.GetComponent<scrObjectLife>().recoveringTime;
            }
        }

        if(other.GetComponent<scrObjectStun>() != null)
        {
            other.GetComponent<scrObjectStun>().SetStunDuration(stunDuration);
        }
    }
}
