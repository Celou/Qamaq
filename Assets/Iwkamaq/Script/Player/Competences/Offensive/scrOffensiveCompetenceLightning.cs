using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrOffensiveCompetenceLightning : MonoBehaviour {

    [Header("Times")]
    public float startingDelay;
    float totalStartingDelay;
    public float lifeTime;
    float totalLifeTime;
    public bool lifeTimeStarted;

    [Header("CompetenceValues")]
    public float lightningKnockback;
    public float lightningStunDuration;
    public float lightningMaxDamages;
    public float lightningCentralZoneSize;

    [Header("Animation")]
    public int preparationAnimNumberOfFrames;
    public int impactAnimNumberOfFrames;
    Animator lightningAnimator;

    public float shakeDelay;
    public float lightningShakeAmount;
    public float lightningShakeDuration;

	void Start () {
        totalLifeTime = lifeTime;
        GetComponent<SphereCollider>().enabled = false;
        lightningAnimator = GetComponent<Animator>();
        lifeTimeStarted = false;
        totalStartingDelay = startingDelay;
    }
	
	void Update () {
        if (lifeTimeStarted && !scrCompetenceMenu.CompetenceMenu.inCompetenceMenu)
            LifeTimeUpdate();

        AnimationUpdate();

        if(!scrCompetenceMenu.CompetenceMenu.inCompetenceMenu)
            LaunchingLightning();

    }

    void LifeTimeUpdate()
    {
        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void AnimationUpdate()
    {
        if (scrCompetenceMenu.CompetenceMenu.inCompetenceMenu)
            lightningAnimator.speed = 0;
        else
        {
            if (!lifeTimeStarted)
            {
                lightningAnimator.speed = preparationAnimNumberOfFrames / totalStartingDelay;
            }
            else
            {
                lightningAnimator.SetBool("impacting", true);
                lightningAnimator.speed = impactAnimNumberOfFrames / totalLifeTime;
            }
        }
    }

    void LaunchingLightning()
    {
        if (startingDelay > 0)
            startingDelay -= Time.deltaTime;

        if (startingDelay < 0)
        {
            startingDelay = 0;

            GetComponent<SphereCollider>().enabled = true;

            lifeTimeStarted = true;

            scrCameraGlobalMovement.CameraManager.SetCameraShake("vertical", lightningShakeAmount, lightningShakeDuration);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<scrObjectKnockback>() != null)
        {
            if (Vector3.Distance(transform.position, other.transform.position) < GetComponent<SphereCollider>().radius * lightningCentralZoneSize)
            {

            }
            else
                other.GetComponent<scrObjectKnockback>().GetKnockback(lightningKnockback, (other.GetComponent<Transform>().position - transform.position).normalized);
        }

        if (other.GetComponent<scrObjectLife>() != null)
        {
            if (other.GetComponent<scrObjectLife>().currentRecoveringTime == 0)
            {
                other.GetComponent<scrObjectLife>().currentLife -= lightningMaxDamages - (Vector3.Distance(transform.position, other.transform.position)/GetComponent<SphereCollider>().radius) * lightningMaxDamages / 2;
                other.GetComponent<scrObjectLife>().currentRecoveringTime = other.GetComponent<scrObjectLife>().recoveringTime;
            }
        }

        if (other.GetComponent<scrObjectStun>() != null)
        {
            if(Vector3.Distance(transform.position, other.transform.position) < GetComponent<SphereCollider>().radius * lightningCentralZoneSize)
                other.GetComponent<scrObjectStun>().SetStunDuration(lightningStunDuration * 4);
            else
                other.GetComponent<scrObjectStun>().SetStunDuration(lightningStunDuration);
        }
    }
}
