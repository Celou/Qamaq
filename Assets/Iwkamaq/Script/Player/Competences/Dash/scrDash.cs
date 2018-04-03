using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrDash : MonoBehaviour {

    [Header("Competence Objects")]
    public Rigidbody playerBody;

    [Header("Competence Values")]
    public bool dashing;
    public float maxDashSpeed;
    float currentDashSpeed;
    public float dashAcceleration;
    Vector3 dashingDirection;

    [Header("Competence Attack Damages")]
    public float dashDamages;
    public float dashKnockback;
    public float dashStunDuration;
    public float dashShakeAmount;
    public float dashShakeDuration;

    [Header("Competences Visuals")]
    public GameObject traceObject;
    public SpriteRenderer playerRenderer;
    int traceCooldown;
    public float traceLifeTime;
    public float traceOffset;

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
        playerBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GetComponent<scrPlayerPaused>().playerPaused)
            return;

        CooldownUpdate();

        DurationCooldown();

        if(dashing)
            CompetenceUpdate();

        AnimationUpdate();

        if (Input.GetKeyDown(KeyCode.E) && currentCooldown == 0 && !GetComponent<scrPlayerPunchUpdate>().punching && !GetComponent<scrPlayerKnockBack>().knockbacked && !GetComponent<scrPlayerStun>().stuned && !GetComponent<scrPlayerPaused>().playerPaused) 
            CompetenceActivation();

    }

    private void FixedUpdate()
    {
        if(dashing && !GetComponent<scrPlayerPaused>().playerPaused)
            playerBody.velocity = dashingDirection * currentDashSpeed * Time.fixedDeltaTime;
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
        gameObject.layer = 19;
        

        currentCooldown = cooldown;
        currentDuration = duration;

        dashing = true;

        dashingDirection = (new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).z) - transform.position).normalized;

    }

    void CompetenceUpdate()
    {
        //playerBody.velocity = dashingDirection * currentDashSpeed * Time.deltaTime;

        //ce qu'il se passe quand la compétence est en cours
        if (currentDuration > 0)
        {
            if (currentDashSpeed < maxDashSpeed)
                currentDashSpeed += dashAcceleration;

            if (currentDashSpeed > maxDashSpeed)
                currentDashSpeed = maxDashSpeed;
        }
        if(currentDuration == 0)
        {
            if (currentDashSpeed > 0)
                currentDashSpeed -= dashAcceleration;

            if (currentDashSpeed < 0)
                currentDashSpeed = 0;
        }

        if (currentDashSpeed == 0 && dashing)
            dashing = false;

    }

    void CompetenceEnd()
    {
        //ce qu'il se passe à la fin de la compétence
        gameObject.layer = 0;

        currentDuration = 0;
        dashingDirection = Vector3.zero;

    }

    void AnimationUpdate()
    {
        if (dashing)
        {
            if (traceCooldown == 0)
            {
                traceCooldown = 3;
                GameObject trace = (GameObject)Instantiate(traceObject, transform.position + new Vector3(0, 0, traceOffset), transform.rotation);

                trace.GetComponent<SpriteRenderer>().sprite = playerRenderer.sprite;
                trace.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 1);
                trace.GetComponent<scrDashTrace>().currentLifeTime = traceLifeTime;
                trace.GetComponent<scrDashTrace>().totalLifeTime = traceLifeTime;
            }

            if (traceCooldown > 0)
                traceCooldown -= 1;
        }
    }
}
