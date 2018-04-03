using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerPunchUpdate : MonoBehaviour {

    [Header("Punch Objects")]
    public GameObject punchObject;
    Rigidbody playerBody;

    [Header ("Global Punch Values")]
    public bool punching;
    float currentPunchImpetus;
    Vector3 punchingDirection;

    [Header("Base Punch Values")]
    public float basePunchImpetus;

    [Header("Final Punch Values")]
    public float finalPunchImpetus;
    public float finalPunchDamagesMultiplicator;

    [Header("Combo")]
    public float punchComboTime;
    float currentPunchComboTime;
    public int currentPunchCombo;

    [Header("Punch Visuals")]
    public float finalPunchScreenshakeAmount;
    public float finalPunchScreenshakeDuration;

    [Header("Cooldowns")]
    public float baseCooldown;
    public float finalCooldown;
    float currentCooldown;

    [Header("Duration")]
    public float baseDuration;
    public float finalDuration;
    public float currentDuration;

    private void Start()
    {
        currentCooldown = 0;
        currentDuration = 0;

        playerBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(!GetComponent<scrPlayerPaused>().playerPaused)
            CooldownUpdate();     
        
        if(!GetComponent<scrPlayerPaused>().playerPaused)
            DurationUpdate();

        AnimationUpdate();

        if(!GetComponent<scrPlayerPaused>().playerPaused)
            ComboTimeUpdate();

        if (Input.GetMouseButtonDown(0) && currentCooldown == 0 && !GetComponent<scrDash>().dashing && !GetComponent<scrPlayerKnockBack>().knockbacked && !GetComponent<scrPlayerStun>().stuned)
            CompetenceActivation();

    }

    void FixedUpdate()
    {
        if (punching && currentPunchCombo > 0 && !GetComponent<scrPlayerPaused>().playerPaused)
            playerBody.velocity = punchingDirection * Time.fixedDeltaTime * basePunchImpetus;

        if (punching && currentPunchCombo == 0 && !GetComponent<scrPlayerPaused>().playerPaused)
            playerBody.velocity = punchingDirection * Time.fixedDeltaTime * finalPunchImpetus;
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

    void DurationUpdate()
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
        punching = true;

        if (currentPunchCombo < 3)
        {
            //coup classique
            currentCooldown = baseCooldown;
            currentDuration = baseDuration;

            currentPunchComboTime = punchComboTime;

            currentPunchCombo += 1;
        }
        else
        {
            //coup final 
            currentCooldown = finalCooldown;
            currentDuration = finalDuration;

            currentPunchComboTime = 0;

            currentPunchCombo = 0;
        }

        punchObject.SetActive(true);

        punchingDirection = (new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).z) - transform.position).normalized;

    }

    void ComboTimeUpdate()
    {
        if (currentPunchComboTime > 0)
            currentPunchComboTime -= Time.deltaTime;

        if (currentPunchComboTime < 0)
        {
            currentPunchComboTime = 0;
            currentPunchCombo = 0;
        }
    }

    void CompetenceUpdate()
    {
        //playerBody.velocity = dashingDirection * currentDashSpeed * Time.deltaTime;

        //ce qu'il se passe quand la compétence est en cours
        if (currentDuration > 0)
        {

        }
        if (currentDuration == 0)
        {

        }

    }

    void CompetenceEnd()
    {
        //ce qu'il se passe à la fin de la compétence
        punching = false ;

        currentDuration = 0;
        punchingDirection = Vector3.zero;
        punchObject.SetActive(false);
    }

    void AnimationUpdate()
    {
        
    }
}
