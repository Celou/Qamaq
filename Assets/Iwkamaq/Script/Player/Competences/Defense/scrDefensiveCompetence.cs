using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrDefensiveCompetence : MonoBehaviour {

    [Header("Competence Objects")]
    //public GameObject protectionSphere;
    public GameObject sphereDamagesStock;
    public GameObject sphereExplosionPrefab;

    [Header("Competence Values")]
    public float maxRadius;
    public float expandSpeed;

    [Header("Competences Visuals")]
    public Animator protectionSphereAnimatorFront;
    public Animator protectionSphereAnimatorBack;

    public float explosionShakingAmount;
    public float explosionShakingDuration;

    [Header("Cooldowns")]
    public float cooldown;
    public float currentCooldown;

    [Header("Duration")]
    public float duration;
    public float currentDuration;

    private void Start()
    {
        currentCooldown = 0;
        currentDuration = 0;
    }

    void Update () {

        if(!GetComponentInParent<scrPlayerPaused>().playerPaused)
            CooldownUpdate();

        if(!GetComponentInParent<scrPlayerPaused>().playerPaused)
            DurationCooldown();

        if (currentDuration > 0 && !GetComponentInParent<scrPlayerPaused>().playerPaused)
            CompetenceUpdate();

        AnimationUpdate();

        if (Input.GetKeyDown(KeyCode.A) && currentDuration > 0 && !GetComponentInParent<scrPlayerPaused>().playerPaused)
            CompetenceEnd();

        if (Input.GetKeyDown(KeyCode.A) && currentCooldown == 0 && !GetComponentInParent<scrPlayerPaused>().playerPaused)
            CompetenceActivation();

    }

    void CooldownUpdate()
    {
        //gère les cooldowns
        if (currentCooldown > 0)
            currentCooldown -= Time.deltaTime;

        if (currentCooldown < 0)
            currentCooldown = 0;

        if (currentCooldown == 0)
        {

        }
    }

    void DurationCooldown()
    {
        //gère la durée. Vérifie aussi si la compétence est bien censée se jouée ou pas

        if (currentDuration > 0)
        {
            currentDuration -= Time.deltaTime;
        }

        if (currentDuration < 0)
        {
            CompetenceEnd();
        }
    }

    public void CompetenceActivation()
    {
        //ce qu'il se passe au lancement de la compétence

        currentCooldown = cooldown;
        currentDuration = duration;

        protectionSphereAnimatorFront.SetBool("protected", true);
        protectionSphereAnimatorBack.SetBool("protected", true);

        GetComponent<SphereCollider>().enabled = true;
        sphereDamagesStock.GetComponent<SphereCollider>().enabled = true;
    }

    void CompetenceUpdate()
    {
        //ce qu'il se passe quand la compétence est en cours
        if (GetComponent<SphereCollider>().radius < maxRadius)
        {
            GetComponent<SphereCollider>().radius += expandSpeed * Time.deltaTime;
        }

        if (GetComponent<SphereCollider>().radius > maxRadius)
        {
            GetComponent<SphereCollider>().radius = maxRadius;
        }
    }

    void CompetenceEnd()
    {
        //ce qu'il se passe à la fin de la compétence

        currentDuration = 0;

        GetComponent<SphereCollider>().radius = 0;

        protectionSphereAnimatorFront.SetBool("protected", false);
        protectionSphereAnimatorBack.SetBool("protected", false);

        GameObject sphereExplosion = (GameObject)Instantiate(sphereExplosionPrefab, transform.position, Quaternion.identity, transform);

        sphereExplosion.GetComponent<scrDefensiveCompetenceExplosion>().paybackDamages = (sphereDamagesStock.GetComponent<scrObjectLife>().maxLife - sphereDamagesStock.GetComponent<scrObjectLife>().currentLife)/10;
        sphereDamagesStock.GetComponent<scrObjectLife>().currentLife = sphereDamagesStock.GetComponent<scrObjectLife>().maxLife;

        scrCameraGlobalMovement.CameraManager.SetCameraShake("both", explosionShakingAmount, explosionShakingDuration);

        GetComponent<SphereCollider>().enabled = false;
        sphereDamagesStock.GetComponent<SphereCollider>().enabled = false;
    }

    void AnimationUpdate()
    {
        if (GetComponentInParent<scrPlayerPaused>().playerPaused)
        {
            protectionSphereAnimatorBack.speed = 0;
            protectionSphereAnimatorFront.speed = 0;
        }
        else
        {
            protectionSphereAnimatorBack.speed = 1;
            protectionSphereAnimatorFront.speed = 1;
        }
    }
}
