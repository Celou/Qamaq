using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class scrPlayer : MonoBehaviour
{

    public SpriteRenderer copieTest;

    [Header("Caractéristiques basiques")]
    public float speed;
    public float walkingSpeed;
    public float sprintSpeed;
    public float recoveringTime;
    public float recoveringTimeCurrent;
    public bool recovering;
    public SpriteRenderer PlayerRenderer;
    public SpriteRenderer ReflectRenderer;
    public Rigidbody Body;
    public Transform Pivot;
    public Animator PlayerAnimator;
    public Animator ReflectAnimator;

    float walkX;
    float walkZ;
    public float accelerationCoeff;
    public Vector3 dir;

    [Header("Life")]
    public float maxLife;
    public float life;
    public GameObject lifeBarObject;
    public Image lifeBarFill;
    public float lifeBarShakeDuration;

    [Header("Heal")]
    public bool healUnlocked;
    public float cooldownHeal;
    float currentCooldownHeal;
    public int healAmount;

    [Header("Punch")]
    public Transform PunchTransform;
    //variables temporelles  
    public float basePunchCooldown;
    public float finalPunchCooldown;
    float currentPunchCooldown;
    public float basePunchDuration;
    public float finalPunchDuration;
    float currentPunchDuration;
    //variables pour l'élan et le combo
    public float basePunchImpetus;
    public float finalPunchImpetus;
    public float punchComboTime;
    float currentPunchComboTime;
    public int currentPunchCombo;
    float currentPunchImpetus;
    //degats bonus
    public float finalPunchDamagesMultiplicator;
    public float currentPunchDamagesMultiplicator;
    //screenshake
    public float finalPunchScreenshakeAmount;
    public float finalPunchScreenshakeDuration;

    [Header("Camera")]
    public Camera mainCamera;
    public float cameraZoomSpeed;
    Vector3 mousePos;

    [Header("Zone à effet")]
    public GameObject ZoneEffect;

    [Header("HoldingObject")]
    public bool holdingObject;
    public GameObject HoldedObject;
    public float holdingObjectDelay;
    public GameObject LittleRock;

    [Header("PlayerDamage")]
    public float basicPlayerDamage;
    public float currentPlayerDamage;

    [Header("Knockback")]
    public float knockbackSlowingDownFactor;
    float knockbackSlowingDownVariable;
    float currentKnockbackForce;
    float takenKnockbackForce;
    Vector3 currentKnockbackDirection;

    [Header("SlowMotion")]
    public float lowSlowMoDuration;
    public float lowSlowMoFactor;
    public float mediumSlowMoDuration;
    public float mediumSlowMoFactor;
    public float highSlowMoDuration;
    public float highSlowMoFactor;
    bool slowMoOn;
    float currentSlowMoFactor;
    float thisSlowMoDuration;
    float currentSlowMoDuration;

    [Header("Stun")]
    public float flashDuration;
    float currentFmashDuration;
    float currentStunDuration;

    [Header("Red Mask Damages Feedback")]
    public GameObject redMask;

    [Header("Globe")]
    public GameObject globeObject;
    public GameObject globeReflect;
    public Animator globeAnimator;
    public Animator globeReflectAnimator;
    public SpriteRenderer globeRenderer;
    public Vector3 normalPosition;
    public float maxGlobeDistance;
    public float globeMaxSpeed;
    public float globeMinSpeed;
    public float globeFollowingPlayerSpeed;
    public float minDistance;
    float globeCurrentSpeed;
    public float globeAcceleration;
    public Vector3 globeAimingPosition;

    [Header("Sounds")]
    public AudioSource attackSound1;
    public AudioSource attackSound2;
    public AudioSource attackSound3;
    public AudioSource finalAttackSound;
    public AudioSource attackHitSound;
    public AudioSource finalAttackHitSound;

    public AudioSource FootStepSound1;
    public AudioSource FootStepSound2;
    public AudioSource FootStepSound3;
    public AudioSource FootStepSound4;
    public AudioSource FootStepSound5;

    public AudioSource hurtSound1;
    public AudioSource hurtSound2;
    public AudioSource hurtSound3;

    public AudioSource globeMouvementSound1;
    public AudioSource globeMouvementSound2;
    public AudioSource globeMouvementSound3;

    public AudioSource yamtaLightningCreationSound;

    public AudioSource protectionSphereCreationSound;
    public AudioSource protectionSphereMaintenanceSound;
    public AudioSource protectionSphereEndSound;
    public AudioSource protectionSphereBreakSound;

    public AudioSource totemInvocationSound;

    public AudioSource boostActivationSound;
    public AudioSource boostMaintenanceSound;
    public AudioSource boostEndSound;


    [Header("PostProcessFX")]

    private float Saturation = 1;
    public PostProcessingProfile PPstack;
    public PostProcessingProfile PPstackB;
    public BloomModel.Settings bloomSettings;
    public ColorGradingModel.Settings colorSettings;
    public ColorGradingModel.Settings colorSettingsB;
    public Camera CamB;
	float Satu = 1;
	float RotAmp = 0 ;
	float RotFreq = 0;
	float ShutAngle = 0;
	float FrameBlending = 0;
		


    void Start()
    {
        PPstackB.colorGrading.enabled = false;
        currentKnockbackForce = 0;
        currentKnockbackDirection = Vector3.zero;
        scrCompetenceManager.CompetenceManager.protectionSphereCollider.radius = 0;

    }




    void Update()
    {

        copieTest.sprite = PlayerRenderer.sprite;

        SlowMoUpdate();

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (!scrExperienceManager.ExperienceManager.inCompetenceMenu && lifeBarObject.GetComponent<Screenshake>().shakeDuration <= 0)
            lifeBarObject.transform.localPosition = new Vector3(9, -310, 10);
        if (scrExperienceManager.ExperienceManager.inCompetenceMenu)
            lifeBarObject.transform.localPosition = new Vector3(9, -310 - 1080, 10);

        if (scrExperienceManager.ExperienceManager.inCompetenceMenu == true)
        {

            Body.velocity = Vector3.zero;
            PlayerAnimator.speed = 0;
            return;

        }

        lifeBarFill.fillAmount = life / maxLife;

        if (scrCompetenceManager.CompetenceManager.protectionSphereOn || scrCompetenceManager.CompetenceManager.vendettaSphereOn || currentPunchDuration > 0 || scrCamera.CameraManager.eventing)
            Body.velocity = Vector3.zero;

        //if(currentPunchDuration <= 0)
        if (currentStunDuration <= 0 && scrCamera.CameraManager.eventing == false)
            MovementUpdate();

        /*if (healUnlocked)
		    Heal ();*/

        Recovering();

        if (currentStunDuration <= 0)
            PunchUpdate();

        AnimationUpdate();

        //PlayerRenderer.transform.position = transform.position;

        if (currentPunchDuration <= 0)
            Regard();

        //if (scrCompetenceManager.CompetenceManager.protectionSphereEquiped)
        ProtectionSphereUpdate();

        //if (scrCompetenceManager.CompetenceManager.yamtaLightningEquiped)
        YamtaLightningUpdate();

        //if (scrCompetenceManager.CompetenceManager.totemEquiped)
        TotemUpdate();

        //if (scrCompetenceManager.CompetenceManager.boostEquiped)
        BoostUpdate();

        if (!scrCompetenceManager.CompetenceManager.boostEquiped && scrCompetenceManager.CompetenceManager.currentBoostDuration < 0)
        {

            /*punchDamage = Mathf.FloorToInt(punchDamage / scrCompetenceManager.CompetenceManager.damageBoostAmount);
            sprintSpeed = Mathf.FloorToInt(sprintSpeed / scrCompetenceManager.CompetenceManager.speedBoostAmont);
            walkingSpeed = Mathf.FloorToInt(walkingSpeed / scrCompetenceManager.CompetenceManager.speedBoostAmont);*/
            scrCompetenceManager.CompetenceManager.currentBoostDuration = 0;
            scrCompetenceManager.CompetenceManager.currentDamageBoostAmount = 1;
            scrCompetenceManager.CompetenceManager.currentSpeedBoostAmount = 1;
            scrCompetenceManager.CompetenceManager.Particles.transform.position = transform.position + new Vector3(0, 20, 0);

        }

        //if (scrCompetenceManager.CompetenceManager.vendettaSphereEquiped)
        VendettaSphereUpdate();

        //if (scrCompetenceManager.CompetenceManager.heavenlyLightningEquiped)
        HeavenlyLightningUpdate();

        if (holdingObject)
            HoldingObject();

        DamageUpdate();

        KnockBackUpdate();

        StunUpdate();

        GlobeUpdate();

        DamageDesaturationUpdate();
    }

    void MovementUpdate()
    {

        //vitesse du joueur         
        if (scrCompetenceManager.CompetenceManager.protectionSphereOn == true || scrCompetenceManager.CompetenceManager.vendettaSphereOn == true)
            return;

        if (Input.GetKey(KeyCode.LeftShift))
            speed = (sprintSpeed) * scrCompetenceManager.CompetenceManager.currentSpeedBoostAmount;
        else
            speed = (walkingSpeed) * scrCompetenceManager.CompetenceManager.currentSpeedBoostAmount;

        //tests pour un nouveau système de déplacements
        if (currentPunchDuration <= 0)
        {

            if (Input.GetKey(KeyCode.Z) && walkZ < 1)
                walkZ += accelerationCoeff;

            if (Input.GetKey(KeyCode.S) && walkZ > -1)
                walkZ -= accelerationCoeff;

            if (Input.GetKey(KeyCode.D) && walkX < 1)
                walkX += accelerationCoeff;

            if (Input.GetKey(KeyCode.Q) && walkX > -1)
                walkX -= accelerationCoeff;
        }

        if (!Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.S) && walkZ != 0)
        {
            walkZ -= Mathf.Sign(walkZ) * accelerationCoeff;
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.Q) && walkX != 0)
            walkX -= Mathf.Sign(walkX) * accelerationCoeff;

        if (Mathf.Abs(walkZ) > 1)
            walkZ = Mathf.Sign(walkZ);

        if (Mathf.Abs(walkX) > 1)
            walkX = Mathf.Sign(walkX);

        if (Mathf.Abs(walkZ) < 0.5f * accelerationCoeff)
            walkZ = 0;

        if (Mathf.Abs(walkX) < 0.5f * accelerationCoeff)
            walkX = 0;

        dir = new Vector3(walkX, 0, walkZ);
        if (Mathf.Abs(walkX) > 0.75f && Mathf.Abs(walkZ) > 0.75f)
            dir = dir.normalized;

        Body.velocity = dir * speed * Time.deltaTime;

    }

    /*void Heal () {
		//permet au joueur de se soigner
		//ce code peut être utilisé pour toute autre capacité ayant un cooldown et/ou un temps d'effet (duration). 
		//currentCooldown est la variable qui se modifie en fct du temps tandis que Cooldown (sans rien devant) est la valeur à tweaker dans l'inspector pour équilibrer

		if(Input.GetKeyDown(KeyCode.C) && currentCooldownHeal <= 0) {

			life+= healAmount;
			currentCooldownHeal = cooldownHeal;

		}

		if (currentCooldownHeal >=0)
			currentCooldownHeal -= Time.deltaTime;

	}*/

    void Recovering()
    {

        if (scrCompetenceManager.CompetenceManager.protectionSphereOn == false && scrCompetenceManager.CompetenceManager.vendettaSphereOn == false)
            PlayerRenderer.color = new Color(1f, 1f, 1f, Mathf.Abs(Mathf.Cos(10 * recoveringTimeCurrent)));
        ReflectRenderer.color = new Color(1f, 1f, 1f, Mathf.Abs(Mathf.Cos(10 * recoveringTimeCurrent)));


        if (recovering == true)
            recoveringTimeCurrent -= Time.deltaTime;

        if (recoveringTimeCurrent < 0)
            recoveringTimeCurrent = 0;

        if (recoveringTimeCurrent == 0)
            recovering = false;

    }

    void PunchUpdate()
    {

        if (currentPunchCooldown > 0)
            currentPunchCooldown -= Time.deltaTime;

        if (currentPunchDuration > 0)
        {
            currentPunchDuration -= Time.deltaTime;
            /*Debug.Log(walkX);
            Debug.Log(walkZ);*/
        }

        if (currentPunchCooldown < 0)
            currentPunchCooldown = 0;

        if (currentPunchDuration < 0)
            currentPunchDuration = 0;


        if (currentPunchDuration == 0)
        {

            PunchTransform.localPosition = new Vector3(0, 0, 10);
            PunchTransform.localScale = new Vector3(0, 0, 0);

        }

        if (currentPunchCooldown == 0 && Input.GetMouseButtonDown(0) && scrCompetenceManager.CompetenceManager.currentGlobalCooldown == 0)
        {
            //gestion du combo et de l'élan
            if (currentPunchCombo < 3)
            {

                currentPunchImpetus = basePunchImpetus;
                currentPunchCombo += 1;
                currentPunchComboTime = punchComboTime;
                currentPunchCooldown = basePunchCooldown;
                currentPunchDuration = basePunchDuration;
            }
            else
            {
                currentPunchImpetus = finalPunchImpetus;
                currentPunchComboTime = punchComboTime;
                currentPunchCombo = 0;
                currentPunchCooldown = finalPunchCooldown;
                currentPunchDuration = finalPunchDuration;
            }

            //arret du perso et appartion de la hitbox de dégats
            Body.velocity = Vector3.zero;

            PunchTransform.localScale = new Vector3(1.5f, 2, 2);
            PunchTransform.localPosition = new Vector3(0, 0.5f, 0.5f);

            if (currentPunchCombo == 1)
                attackSound1.Play();

            if (currentPunchCombo == 2)
                attackSound2.Play();

            if (currentPunchCombo == 3)
                attackSound3.Play();

            if (currentPunchCombo == 0)
                finalAttackSound.Play();

        }

        //gestion du combo
        if (currentPunchComboTime > 0)
            currentPunchComboTime -= Time.deltaTime;

        if (currentPunchComboTime < 0)
        {
            currentPunchComboTime = 0;
            currentPunchCombo = 0;
        }

        if (currentPunchDuration > 0 && currentPunchCombo == 0)
        {
            currentPunchDamagesMultiplicator = finalPunchDamagesMultiplicator;
        }
        else
        {
            currentPunchDamagesMultiplicator = 1;
        }

        //gestion de l'élan
        if (currentPunchImpetus > 0 && currentPunchDuration > 0)
        {
            //transform.Translate(Pivot.up * currentPunchImpetus * Time.deltaTime, Space.World);
            Body.velocity = Pivot.up * currentPunchImpetus * Time.deltaTime;

        }


    }

    void HoldingObject()
    {

        if (holdingObjectDelay > 0)
            holdingObjectDelay -= Time.deltaTime;

        if (HoldedObject == LittleRock && Input.GetKeyDown(KeyCode.F) && holdingObjectDelay < 0)
        {

            LittleRock.GetComponent<scrLittleRock>().enabled = true;
            LittleRock.GetComponent<scrLittleRock>().transform.rotation = Pivot.rotation;
            LittleRock.GetComponent<scrLittleRock>().throwed = true;
            LittleRock.GetComponent<scrLittleRock>().currentLifeTime = LittleRock.GetComponent<scrLittleRock>().lifeTime;
            holdingObject = false;
            return;

        }

        HoldedObject.transform.position = transform.position + new Vector3(0, 0, 1.25f);

    }

    void Regard()
    {

        //cette fonction sert à diriger à chaque instant le regard du joueur vers la souris

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.y = 0;
        Vector3 dir = mousePos - transform.position;

        float rotY = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        Pivot.rotation = Quaternion.Euler(90, rotY, 0);

    }

    void SpeedUpdate()
    {



    }

    void DamageUpdate()
    {

        currentPlayerDamage = (basicPlayerDamage) * scrCompetenceManager.CompetenceManager.currentDamageBoostAmount;

    }

    void OnTriggerStay(Collider other)
    {

        #region Enemies
        //pour les ennemis, on commence par obtenir les dégats et le knocback (manager), puis on vérifie si l'ennemi attaque (getcomponent) ainsi que la direction de Knockback (getcomponent), et enfin, on règle
        float damagesTaken = scrDamagesManager.DamagesManager.GetEnemyDamages(other.tag);
        float knockbackForceTaken = scrDamagesManager.DamagesManager.GetEnemyKnockBackForce(other.tag);
        float stunDurationTaken = scrDamagesManager.DamagesManager.GetEnemyStunDuration(other.tag);
        int randomSoundVariable = Random.Range(0, 3);

        //on verifie que l'ennemi en question soit bien en train d'attaquer
        bool enemyAttacking = false;
        if (other.tag == "monkey")
            enemyAttacking = other.GetComponent<scrEnemyBase>().attacking;
        if (other.tag == "crawler")
            enemyAttacking = true;

        #region gestion des dégats
        if (damagesTaken != 0)
        {


            #region dummies
            if (other.tag == "enemy1" || other.tag == "enemy3")
                enemyAttacking = true;

            #endregion

            //le joueur n'est pas immunisé (ni recover, ni protection,...) : il prend des dégats
            if (scrCompetenceManager.CompetenceManager.protectionSphereOn == false && scrCompetenceManager.CompetenceManager.vendettaSphereOn == false && recovering == false && enemyAttacking)
            {
                life -= damagesTaken;
                recovering = true;
                recoveringTimeCurrent = recoveringTime;
                redMask.GetComponent<scrRedMaskDamages>().baseRedDuration = 1;
                redMask.GetComponent<scrRedMaskDamages>().currentRedDuration = redMask.GetComponent<scrRedMaskDamages>().baseRedDuration;
                if (randomSoundVariable == 0)
                {
                    hurtSound1.Play();
                }
                if (randomSoundVariable == 1)
                {
                    hurtSound2.Play();
                }
                if (randomSoundVariable == 2)
                {
                    hurtSound3.Play();
                }

                //mort
                if (life <= 0)
                {
                    Respawn();
                    return;
                }

                #region slowMo
                if (damagesTaken >= 10 && damagesTaken < 20 && enemyAttacking)
                    StartCoroutine(SlowMotion(lowSlowMoDuration, lowSlowMoFactor));
                if (damagesTaken >= 20 && damagesTaken < 30 && enemyAttacking)
                    StartCoroutine(SlowMotion(mediumSlowMoDuration, mediumSlowMoFactor));
                if (damagesTaken >= 30 && damagesTaken < 40 && enemyAttacking)
                    StartCoroutine(SlowMotion(highSlowMoDuration, highSlowMoFactor));
                #endregion

                #region gestion knockback
                if (knockbackForceTaken != 0 && damagesTaken != 0 && enemyAttacking)
                {
                    takenKnockbackForce = knockbackForceTaken;

                    Vector3 knockbackDirection = Vector3.zero;

                    if (other.tag == "monkey" && enemyAttacking)
                    {
                        knockbackDirection = (transform.position - other.GetComponent<scrEnemyBase>().transform.position).normalized;
                        knockbackDirection = new Vector3(-knockbackDirection.z, 0, knockbackDirection.x);
                        //knockbackDirection = new Vector3(other.GetComponent<scrEnemyBase>().transform.rotation.x, other.GetComponent<scrEnemyBase>().transform.rotation.y, other.GetComponent<scrEnemyBase>().transform.rotation.z);
                        other.GetComponent<BoxCollider>().enabled = false;
                    }
                    //if (other.tag == "enemy1")
                    //    knockbackDirection = (transform.position - new Vector3(240.93f, 0, -36.64f)).normalized;

                    currentKnockbackDirection = new Vector3(knockbackDirection.x, 0, knockbackDirection.z);
                    currentKnockbackForce = knockbackForceTaken;

                }
                #endregion

                #region autres feedbacks
                //duree du stun

                currentStunDuration = stunDurationTaken;

                //shake de la barre de dégats
                if (damagesTaken != 0 && enemyAttacking)
                {
                    lifeBarObject.GetComponent<Screenshake>().shakeDuration = lifeBarShakeDuration;
                    lifeBarObject.GetComponent<Screenshake>().shakeAmount = Mathf.Clamp(damagesTaken / 2, 0, 10);
                }

                #endregion
            }

            //le joueur a activé sa sphère de vendetta : elle emmagasine les dégats
            if (scrCompetenceManager.CompetenceManager.vendettaSphereOn == true && enemyAttacking)
            {

                scrCompetenceManager.CompetenceManager.vendattaShereDamageStockage += damagesTaken;

                if (scrCompetenceManager.CompetenceManager.vendattaShereDamageStockage >= scrCompetenceManager.CompetenceManager.vendettaSphereProtectionAmount)
                {

                    scrCompetenceManager.CompetenceManager.vendattaShereDamageStockage = scrCompetenceManager.CompetenceManager.vendettaSphereProtectionAmount;
                    scrCompetenceManager.CompetenceManager.currentVendettaSphereDuration = -1;

                }

            }



        }
        #endregion



        //mort du joueur?
        /* if (life <= 0)
            SceneManager.LoadScene("FirstLevel");*/


        if (other.tag == "gorillaRock")
        {

            int damage = scrEnemyManager.EnemyManager.gorillaRockDamage;

            if (scrCompetenceManager.CompetenceManager.vendettaSphereOn == true)
            {

                scrCompetenceManager.CompetenceManager.vendattaShereDamageStockage += damage;

                if (scrCompetenceManager.CompetenceManager.vendattaShereDamageStockage >= scrCompetenceManager.CompetenceManager.vendettaSphereProtectionAmount)
                {

                    scrCompetenceManager.CompetenceManager.vendattaShereDamageStockage = scrCompetenceManager.CompetenceManager.vendettaSphereProtectionAmount;
                    scrCompetenceManager.CompetenceManager.currentVendettaSphereDuration = -1;

                }

            }

            if (scrCompetenceManager.CompetenceManager.protectionSphereOn == false && scrCompetenceManager.CompetenceManager.vendettaSphereOn == false && recovering == false)
            {

                life -= damage;
                recovering = true;
                recoveringTimeCurrent = recoveringTime;

            }


            /*if (life <= 0)
                SceneManager.LoadScene("FirstLevel");*/
            //Destroy(gameObject);

        }

        if (other.tag == "gorillaContactDamage")
        {
            Debug.Log("salut");
            int damage = scrEnemyManager.EnemyManager.gorillaContactDamage;


            if (scrCompetenceManager.CompetenceManager.vendettaSphereOn == true)
            {

                scrCompetenceManager.CompetenceManager.vendattaShereDamageStockage += damage;

                if (scrCompetenceManager.CompetenceManager.vendattaShereDamageStockage >= scrCompetenceManager.CompetenceManager.vendettaSphereProtectionAmount)
                {

                    scrCompetenceManager.CompetenceManager.vendattaShereDamageStockage = scrCompetenceManager.CompetenceManager.vendettaSphereProtectionAmount;
                    scrCompetenceManager.CompetenceManager.currentVendettaSphereDuration = -1;

                }

            }

            if (scrCompetenceManager.CompetenceManager.protectionSphereOn == false && scrCompetenceManager.CompetenceManager.vendettaSphereOn == false && recovering == false)
            {

                life -= damage;
                recovering = true;
                recoveringTimeCurrent = recoveringTime;
                transform.Translate(new Vector3(Mathf.Rad2Deg * scrEnemyManager.EnemyManager.gorillaPivot.rotation.x, 0, Mathf.Rad2Deg * scrEnemyManager.EnemyManager.gorillaPivot.rotation.z) * 0.2f, Space.World);
                Debug.Log(scrEnemyManager.EnemyManager.gorillaPivot.rotation.x);
                Debug.Log(scrEnemyManager.EnemyManager.gorillaPivot.rotation.z);

            }
            #endregion

            /*if (life <= 0)
                Destroy(gameObject);*/

        }

        #region Props
        if (other.tag == "healingFruit")
        {
            if (life < maxLife)
            {
                life += 10;
                Destroy(other.gameObject);

                if (life > maxLife)
                    life = maxLife;

            }
        }
        if (other.tag == "littleRock" && Input.GetKeyDown(KeyCode.F) && holdingObject == false)
        {

            Destroy(other.gameObject);
            holdingObject = true;
            HoldedObject = LittleRock;
            holdingObjectDelay = 0.1f;

        }
        #endregion

    }

    IEnumerator KnockBack(float knockBackForce, Vector3 knockBackDirection)
    {
        while (knockBackForce > 0)
        {
            //fait baisser le knockback
            knockBackForce -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
            //transform.Translate(knockBackDirection * knockBackForce * Time.deltaTime, Space.World);
            Body.velocity = knockBackDirection * knockBackForce * Time.deltaTime;

        }
        Body.velocity = Vector3.zero;
    }

    void KnockBackUpdate()
    {
        // fin du knockback quand la force tombe à z&ro
        if (currentKnockbackForce < 0)
        {
            currentKnockbackForce = 0;
            knockbackSlowingDownVariable = 0;
        }

        if (currentKnockbackForce > 0)
        {
            //la valeur qui fait diminuer la force du knockback augmente plus rapidement quand la force est basse, donc plus la force est basse, plus elle diminue vite
            if (currentKnockbackForce / takenKnockbackForce > 2 / 3)
                knockbackSlowingDownVariable += takenKnockbackForce / 15;
            if (currentKnockbackForce / takenKnockbackForce > 1 / 3 && currentKnockbackForce <= 2 / 3)
                knockbackSlowingDownVariable += takenKnockbackForce / 10;
            if (currentKnockbackForce / takenKnockbackForce <= 1 / 3)
                knockbackSlowingDownVariable += takenKnockbackForce / 5;

            //on déplace le joueur selon le knockback
            //transform.Translate(4 * currentKnockbackDirection * currentKnockbackForce * Time.deltaTime, Space.World);
            Body.velocity = 4 * currentKnockbackDirection * currentKnockbackForce * Time.deltaTime;

            //on fait diminuer la force du Knockback
            currentKnockbackForce -= knockbackSlowingDownVariable * Time.deltaTime;

        }

    }

    IEnumerator SlowMotion(float slowMoDuration, float slowMoFactor)
    {
        //lance le slowMo
        slowMoOn = true;
        currentSlowMoFactor = slowMoFactor;
        currentSlowMoDuration = slowMoDuration;
        yield return 0;
    }

    void SlowMoUpdate()
    {
        //lancement du slowMo
        if (slowMoOn && Time.timeScale == 1)
        {

            Time.timeScale = currentSlowMoFactor;
            Time.fixedDeltaTime = currentSlowMoFactor * 0.02f;

        }

        //atténuation du slowMo
        if (Time.timeScale < 1)
            Time.timeScale += (1f / currentSlowMoDuration) * Time.unscaledDeltaTime;

        //fin du slowMo
        if (slowMoOn && Time.timeScale > 1)
        {
            slowMoOn = false;
            Time.timeScale = 1;
        }



    }

    void StunUpdate()
    {

        if (currentStunDuration > 0)
            currentStunDuration -= Time.deltaTime;

        if (currentStunDuration < 0)
            currentStunDuration = 0;

    }

    void GlobeUpdate()
    {

        #region premier test globe
        //attaché au pivot

        /*globeObject.transform.rotation = Quaternion.Euler(90, 0, 0);

        if(Vector3.Distance(globeObject.transform.localPosition, normalPosition) < maxGlobeDistance)
        {
            Debug.Log("papillonnage");
            globeObject.transform.localPosition += new Vector3(Random.Range(-globeSpeed, globeSpeed) * Time.deltaTime, Random.Range(-globeSpeed, globeSpeed) * Time.deltaTime, 0);
        }
        else
        {

            if (Mathf.Abs(normalPosition.x - globeObject.transform.localPosition.x) < Mathf.Abs(normalPosition.y - globeObject.transform.localPosition.y))
            {

                if (globeObject.transform.localPosition.x > 0)
                    globeObject.transform.localPosition += new Vector3(Random.Range(-globeSpeed, 0) * Time.deltaTime, Random.Range(-globeSpeed, globeSpeed) * Time.deltaTime, 0);
                else
                    globeObject.transform.localPosition += new Vector3(Random.Range(0 , globeSpeed) * Time.deltaTime, Random.Range(-globeSpeed, globeSpeed) * Time.deltaTime, 0);

            }
            else
            {

                if (globeObject.transform.localPosition.y > 0)
                    globeObject.transform.localPosition += new Vector3(Random.Range(-globeSpeed, globeSpeed) * Time.deltaTime, Random.Range(-globeSpeed, 0) * Time.deltaTime, 0);
                else
                    globeObject.transform.localPosition += new Vector3(Random.Range(-globeSpeed, globeSpeed) * Time.deltaTime, Random.Range(0, globeSpeed) * Time.deltaTime, 0);

            }

        }*/
        #endregion

        #region deuxieme test globe
        //suit le curseur avec un distance maximum du personnage

        //on détermine la position que doit occuper le globe
        if (Vector3.Distance(transform.position, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).z)) < maxGlobeDistance)
        {
            globeAimingPosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
        }
        else
        {
            globeAimingPosition = transform.position + Pivot.up * maxGlobeDistance;
        }

        //on déplace le globe vers la position cible, s'il n'y est pas encore
        if (Vector3.Distance(globeObject.transform.position, globeAimingPosition) > globeCurrentSpeed * Time.deltaTime)
            globeObject.transform.Translate((globeAimingPosition - globeObject.transform.position).normalized * globeCurrentSpeed * Time.deltaTime, Space.World);

        //gestion de la vitesse du globe
        if (Vector3.Distance(transform.position, globeObject.transform.position) <= maxGlobeDistance)
        {
            if (Vector3.Distance(globeObject.transform.position, globeAimingPosition) > minDistance && globeCurrentSpeed < globeMaxSpeed)
                globeCurrentSpeed += globeAcceleration;
            if (globeCurrentSpeed > globeMaxSpeed)
                globeCurrentSpeed = globeMaxSpeed;

            if (Vector3.Distance(globeObject.transform.position, globeAimingPosition) < minDistance && globeCurrentSpeed > globeMinSpeed)
                globeCurrentSpeed -= globeAcceleration;
            if (globeCurrentSpeed < globeMinSpeed)
                globeCurrentSpeed = globeMinSpeed;
        }

        else
        {
            if (globeCurrentSpeed < globeFollowingPlayerSpeed)
                globeCurrentSpeed += globeAcceleration;
            if (globeCurrentSpeed > globeFollowingPlayerSpeed)
                globeCurrentSpeed = globeFollowingPlayerSpeed;
        }
        #endregion

        if (!globeMouvementSound1.isPlaying && !globeMouvementSound2.isPlaying && !globeMouvementSound3.isPlaying && globeCurrentSpeed == globeMaxSpeed)
        {
            int randomGlobeMouvementSoundVariable = Random.Range(0, 3);

            if (randomGlobeMouvementSoundVariable == 0)
                globeMouvementSound1.Play();

            if (randomGlobeMouvementSoundVariable == 1)
                globeMouvementSound2.Play();

            if (randomGlobeMouvementSoundVariable == 2)
                globeMouvementSound3.Play();
        }
        if (globeCurrentSpeed < globeMaxSpeed / 2 && (globeMouvementSound1.isPlaying || globeMouvementSound2.isPlaying || globeMouvementSound3.isPlaying))
        {
            globeMouvementSound1.Play();
            globeMouvementSound2.Play();
            globeMouvementSound3.Play();
        }

    }

    void Respawn()
    {

        transform.position = scrCheckPointManager.CheckpointManager.activeCheckPoint.transform.position - new Vector3(0, 0, scrCheckPointManager.CheckpointManager.activeCheckPoint.transform.localScale.y / 2 + 0.5f);
        life = maxLife;
        slowMoOn = false;
        Time.timeScale = 1;
        PlayerAnimator.Play("idleFace");

    }

    void YamtaLightning()
    {
        Vector3 lightningDir = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).z) - globeObject.transform.position;

        float rotY = Mathf.Atan2(lightningDir.x, lightningDir.z) * Mathf.Rad2Deg;

        //Pivot.rotation = Quaternion.Euler(90, rotY, 0);

        scrLightningManager.GetNewLightning(globeObject.transform.position, Quaternion.Euler(90, rotY, 0));

        globeAnimator.Play("globeYamtaLightning");
        globeReflectAnimator.Play("globeYamtaLightning");

        if (lightningDir.z > 0)
        {
            globeRenderer.flipY = true;
            //haut droite
            if (lightningDir.x > 0)
            {
                Debug.Log("haut droite");
                globeRenderer.flipX = true;

            }
            //haut gauche
            else
            {
                Debug.Log("haut gauche");
                globeRenderer.flipX = false;

            }

        }
        else
        {
            globeRenderer.flipY = false;
            //bas droite      
            if (lightningDir.x > 0)
            {
                Debug.Log("bas droite");
                globeRenderer.flipX = true;

            }
            //bash gauche
            else
            {
                Debug.Log("bas gauche");
                globeRenderer.flipX = false;
            }

        }

    }

    void YamtaLightningUpdate()
    {

        if (((scrCompetenceManager.CompetenceManager.slotOneName == scrCompetenceManager.CompetenceManager.yamtaLightningName && Input.GetAxis("Slot One Input") != 0)
            || (scrCompetenceManager.CompetenceManager.slotTwoName == scrCompetenceManager.CompetenceManager.yamtaLightningName && Input.GetAxis("Slot Two Input") != 0)
            || (scrCompetenceManager.CompetenceManager.slotThreeName == scrCompetenceManager.CompetenceManager.yamtaLightningName && Input.GetAxis("Slot Three Input") != 0))
            && (scrCompetenceManager.CompetenceManager.currentYamtaLightningCooldown <= 0)
            && scrCompetenceManager.CompetenceManager.currentGlobalCooldown == 0)
        {

            YamtaLightning();
            scrCompetenceManager.CompetenceManager.currentYamtaLightningCooldown = scrCompetenceManager.CompetenceManager.yamtaLightningCooldown;
            scrCompetenceManager.CompetenceManager.currentGlobalCooldown = scrCompetenceManager.CompetenceManager.yamtaLightningGlobalCooldown;

            yamtaLightningCreationSound.Play();

        }

        if (scrCompetenceManager.CompetenceManager.currentYamtaLightningCooldown > 0)
            scrCompetenceManager.CompetenceManager.currentYamtaLightningCooldown -= Time.deltaTime;

        if (scrCompetenceManager.CompetenceManager.currentYamtaLightningCooldown < 0)
            scrCompetenceManager.CompetenceManager.currentYamtaLightningCooldown = 0;

    }

    void ProtectionSphereUpdate()
    {
        //permet au joueur de devenir invincible
        //ce code peut être utilisé pour toute autre capacité ayant un cooldown et/ou un temps d'effet (duration). 
        //currentCooldown est la variable qui se modifie en fct du temps tandis que Cooldown (sans rien devant) est la valeur à tweaker dans l'inspector pour équilibrer

        if (scrCompetenceManager.CompetenceManager.protectionSphereOn == false
            && ((scrCompetenceManager.CompetenceManager.slotOneName == scrCompetenceManager.CompetenceManager.protectionSphereName && Input.GetAxis("Slot One Input") != 0)
                || (scrCompetenceManager.CompetenceManager.slotTwoName == scrCompetenceManager.CompetenceManager.protectionSphereName && Input.GetAxis("Slot Two Input") != 0)
                || (scrCompetenceManager.CompetenceManager.slotThreeName == scrCompetenceManager.CompetenceManager.protectionSphereName && Input.GetAxis("Slot Three Input") != 0))
            && scrCompetenceManager.CompetenceManager.currentCooldownProtectionSphere <= 0
            && scrCompetenceManager.CompetenceManager.currentGlobalCooldown == 0)
        {

            scrCompetenceManager.CompetenceManager.currentProtectionSphereDuration = scrCompetenceManager.CompetenceManager.protectionSphereDuration;
            scrCompetenceManager.CompetenceManager.protectionSphereOn = true;
            scrCompetenceManager.CompetenceManager.currentCooldownProtectionSphere = scrCompetenceManager.CompetenceManager.cooldownProtectionSphere;
            //PlayerRenderer.color = new Color(0f, 0.5f, 1f);
            scrCompetenceManager.CompetenceManager.currentGlobalCooldown = scrCompetenceManager.CompetenceManager.protectionSphereGlobalCooldown;
            scrCompetenceManager.CompetenceManager.competenceKeyDown = true;


            protectionSphereCreationSound.Play();

        }

        if (scrCompetenceManager.CompetenceManager.currentCooldownProtectionSphere > 0)
            scrCompetenceManager.CompetenceManager.currentCooldownProtectionSphere -= Time.deltaTime;

        if (scrCompetenceManager.CompetenceManager.currentProtectionSphereDuration > 0)
        {
            Body.isKinematic = true;

            scrCompetenceManager.CompetenceManager.currentProtectionSphereDuration -= Time.deltaTime;

            if (scrCompetenceManager.CompetenceManager.competenceKeyDown == true &&
                ((scrCompetenceManager.CompetenceManager.slotOneName == scrCompetenceManager.CompetenceManager.protectionSphereName && Input.GetAxis("Slot One Input") == 0)
                    || (scrCompetenceManager.CompetenceManager.slotTwoName == scrCompetenceManager.CompetenceManager.protectionSphereName && Input.GetAxis("Slot Two Input") == 0)
                    || (scrCompetenceManager.CompetenceManager.slotThreeName == scrCompetenceManager.CompetenceManager.protectionSphereName && Input.GetAxis("Slot Three Input") == 0)))
                scrCompetenceManager.CompetenceManager.competenceKeyDown = false;

            if (scrCompetenceManager.CompetenceManager.competenceKeyDown == false
                && ((scrCompetenceManager.CompetenceManager.slotOneName == scrCompetenceManager.CompetenceManager.protectionSphereName && Input.GetAxis("Slot One Input") != 0)
                    || (scrCompetenceManager.CompetenceManager.slotTwoName == scrCompetenceManager.CompetenceManager.protectionSphereName && Input.GetAxis("Slot Two Input") != 0)
                    || (scrCompetenceManager.CompetenceManager.slotThreeName == scrCompetenceManager.CompetenceManager.protectionSphereName && Input.GetAxis("Slot Three Input") != 0)))
            {
                scrCompetenceManager.CompetenceManager.currentProtectionSphereDuration = -0.1f;
                scrCompetenceManager.CompetenceManager.currentGlobalCooldown = 0.1f;
            }

            if (scrCompetenceManager.CompetenceManager.protectionSphereCollider.radius < scrCompetenceManager.CompetenceManager.protectionSphereColliderMaxRadius)
                scrCompetenceManager.CompetenceManager.protectionSphereCollider.radius += scrCompetenceManager.CompetenceManager.protectionSphereColliderExpandSpeed;
            else
                scrCompetenceManager.CompetenceManager.protectionSphereCollider.radius = scrCompetenceManager.CompetenceManager.protectionSphereColliderMaxRadius;
        }


        if (scrCompetenceManager.CompetenceManager.currentProtectionSphereDuration < 0)
        {

            scrCompetenceManager.CompetenceManager.protectionSphereOn = false;
            scrCompetenceManager.CompetenceManager.currentProtectionSphereDuration = 0;
            scrCompetenceManager.CompetenceManager.protectionSphereCollider.radius = 0;
            Body.isKinematic = false;
            //PlayerRenderer.color = new Color(0f, 0.5f, 1f);

        }

        if (scrCompetenceManager.CompetenceManager.currentProtectionSphereDuration > 0 && !protectionSphereMaintenanceSound.isPlaying)
        {
            protectionSphereMaintenanceSound.Play();
        }
        if (scrCompetenceManager.CompetenceManager.currentProtectionSphereDuration <= 0 && protectionSphereMaintenanceSound.isPlaying)
        {
            protectionSphereMaintenanceSound.Stop();
            protectionSphereEndSound.Play();
        }

    }

    void TotemUpdate()
    {
        //permet au joueur de devenir invincible
        //ce code peut être utilisé pour toute autre capacité ayant un cooldown et/ou un temps d'effet (duration). 
        //currentCooldown est la variable qui se modifie en fct du temps tandis que Cooldown (sans rien devant) est la valeur à tweaker dans l'inspector pour équilibrer

        if (scrCompetenceManager.CompetenceManager.totemSummoned == false
            && ((scrCompetenceManager.CompetenceManager.slotOneName == scrCompetenceManager.CompetenceManager.totemName && Input.GetAxis("Slot One Input") != 0)
                || (scrCompetenceManager.CompetenceManager.slotTwoName == scrCompetenceManager.CompetenceManager.totemName && Input.GetAxis("Slot Two Input") != 0)
                || (scrCompetenceManager.CompetenceManager.slotThreeName == scrCompetenceManager.CompetenceManager.totemName && Input.GetAxis("Slot Three Input") != 0))
            && scrCompetenceManager.CompetenceManager.currentTotemCooldown <= 0
            && scrCompetenceManager.CompetenceManager.currentGlobalCooldown == 0)
        {

            scrCompetenceManager.CompetenceManager.currentTotemDuration = scrCompetenceManager.CompetenceManager.totemDuration;
            scrCompetenceManager.CompetenceManager.totemSummoned = true;
            scrCompetenceManager.CompetenceManager.currentTotemCooldown = scrCompetenceManager.CompetenceManager.totemCooldown;
            scrCompetenceManager.CompetenceManager.taunted = true;
            scrCompetenceManager.CompetenceManager.currentTotemLife = scrCompetenceManager.CompetenceManager.maxTotemLife;
            scrCompetenceManager.CompetenceManager.totemPosition.position = mousePos;
            scrCompetenceManager.CompetenceManager.currentGlobalCooldown = scrCompetenceManager.CompetenceManager.totemGlobalCooldown;

            totemInvocationSound.Play();
        }

        if (scrCompetenceManager.CompetenceManager.currentTotemCooldown > 0)
            scrCompetenceManager.CompetenceManager.currentTotemCooldown -= Time.deltaTime;

        if (scrCompetenceManager.CompetenceManager.currentTotemDuration > 0)
            scrCompetenceManager.CompetenceManager.currentTotemDuration -= Time.deltaTime;


        if (scrCompetenceManager.CompetenceManager.currentTotemDuration < 0)
        {
            scrCompetenceManager.CompetenceManager.totemSummoned = false;
            scrCompetenceManager.CompetenceManager.currentTotemDuration = 0;

        }

        if (scrCompetenceManager.CompetenceManager.currentTotemDuration == 0)
        {

            scrCompetenceManager.CompetenceManager.totemSummoned = false;

        }

        if (scrCompetenceManager.CompetenceManager.totemSummoned == false)
        {

            scrCompetenceManager.CompetenceManager.taunted = false;
            scrCompetenceManager.CompetenceManager.currentTotemLife = 0;
            scrCompetenceManager.CompetenceManager.totemPosition.position = transform.position + new Vector3(0, 20, 0);

        }
    }

    void BoostUpdate()
    {
        //permet au joueur de devenir invincible
        //ce code peut être utilisé pour toute autre capacité ayant un cooldown et/ou un temps d'effet (duration). 
        //currentCooldown est la variable qui se modifie en fct du temps tandis que Cooldown (sans rien devant) est la valeur à tweaker dans l'inspector pour équilibrer

        if (((scrCompetenceManager.CompetenceManager.slotOneName == scrCompetenceManager.CompetenceManager.boostName && Input.GetAxis("Slot One Input") != 0) || (scrCompetenceManager.CompetenceManager.slotTwoName == scrCompetenceManager.CompetenceManager.boostName && Input.GetAxis("Slot Two Input") != 0) || (scrCompetenceManager.CompetenceManager.slotThreeName == scrCompetenceManager.CompetenceManager.boostName && Input.GetAxis("Slot Three Input") != 0)) && scrCompetenceManager.CompetenceManager.currentBoostCooldown <= 0 && scrCompetenceManager.CompetenceManager.currentGlobalCooldown == 0)
        {
            scrCompetenceManager.CompetenceManager.currentBoostDuration = scrCompetenceManager.CompetenceManager.boostDuration;
            scrCompetenceManager.CompetenceManager.currentBoostCooldown = scrCompetenceManager.CompetenceManager.boostCooldown;
            scrCompetenceManager.CompetenceManager.currentDamageBoostAmount = scrCompetenceManager.CompetenceManager.damageBoostAmount;
            scrCompetenceManager.CompetenceManager.currentSpeedBoostAmount = scrCompetenceManager.CompetenceManager.speedBoostAmont;

            scrCompetenceManager.CompetenceManager.Particles.transform.position = transform.position;
            scrCompetenceManager.CompetenceManager.currentGlobalCooldown = scrCompetenceManager.CompetenceManager.boostGlobalCooldown;
			mainCamera.GetComponent<Screenshake>().shakeDuration = 1.2f;
			mainCamera.GetComponent<Screenshake>().shakeAmount = 0.5f;
			mainCamera.GetComponent<Screenshake>().decreaseFactor = 6f;

	
            //boostActivationSound.Play();

        }

        if (scrCompetenceManager.CompetenceManager.currentBoostCooldown > 0)
            scrCompetenceManager.CompetenceManager.currentBoostCooldown -= Time.deltaTime;

        if (scrCompetenceManager.CompetenceManager.currentBoostDuration > 0)
            scrCompetenceManager.CompetenceManager.currentBoostDuration -= Time.deltaTime;


        if (scrCompetenceManager.CompetenceManager.currentBoostDuration < 0)
        {
            scrCompetenceManager.CompetenceManager.currentBoostDuration = 0;
            scrCompetenceManager.CompetenceManager.Particles.transform.position = transform.position + new Vector3(0, 20, 0);
            scrCompetenceManager.CompetenceManager.currentDamageBoostAmount = 1;
            scrCompetenceManager.CompetenceManager.currentSpeedBoostAmount = 1;
        }

        //son : maintenance et arrêt
        if (scrCompetenceManager.CompetenceManager.currentBoostDuration > 0 && !boostMaintenanceSound.isPlaying)
        {
            boostMaintenanceSound.Play();
        }
        if (scrCompetenceManager.CompetenceManager.currentBoostDuration <= 0 && boostMaintenanceSound.isPlaying)
        {
            boostMaintenanceSound.Stop();
            boostEndSound.Play();
        }
    }

    void VendettaSphereUpdate()
    {
        //permet au joueur de devenir invincible puis de renvoyer les dégats
        //ce code peut être utilisé pour toute autre capacité ayant un cooldown et/ou un temps d'effet (duration). 
        //currentCooldown est la variable qui se modifie en fct du temps tandis que Cooldown (sans rien devant) est la valeur à tweaker dans l'inspector pour équilibrer


        //lancer la protection
        if (scrCompetenceManager.CompetenceManager.vendettaSphereOn == false && (scrCompetenceManager.CompetenceManager.slotOneName == scrCompetenceManager.CompetenceManager.vendettaSphereName && Input.GetAxis("Slot One Input") != 0) || (scrCompetenceManager.CompetenceManager.slotTwoName == scrCompetenceManager.CompetenceManager.vendettaSphereName && Input.GetAxis("Slot Two Input") != 0) || (scrCompetenceManager.CompetenceManager.slotThreeName == scrCompetenceManager.CompetenceManager.vendettaSphereName && Input.GetAxis("Slot Three Input") != 0) && scrCompetenceManager.CompetenceManager.currentCooldownVendettaSphere <= 0 && scrCompetenceManager.CompetenceManager.currentGlobalCooldown == 0)
        {

            scrCompetenceManager.CompetenceManager.currentVendettaSphereDuration = scrCompetenceManager.CompetenceManager.vendettaSphereDuration;
            scrCompetenceManager.CompetenceManager.vendettaSphereOn = true;
            scrCompetenceManager.CompetenceManager.currentCooldownVendettaSphere = scrCompetenceManager.CompetenceManager.cooldownVendettaSphere;
            PlayerRenderer.color = new Color(0f, 0.5f, 1f);
            ReflectRenderer.color = new Color(0f, 0.5f, 1f);
            scrCompetenceManager.CompetenceManager.currentGlobalCooldown = scrCompetenceManager.CompetenceManager.vendettaSphereGlobalCooldown;

        }

        if (scrCompetenceManager.CompetenceManager.currentCooldownVendettaSphere > 0)
            scrCompetenceManager.CompetenceManager.currentCooldownVendettaSphere -= Time.deltaTime;

        if (scrCompetenceManager.CompetenceManager.currentCooldownVendettaSphere < 0)
            scrCompetenceManager.CompetenceManager.currentCooldownVendettaSphere = 0;

        if (scrCompetenceManager.CompetenceManager.currentVendettaSphereDuration > 0)
            scrCompetenceManager.CompetenceManager.currentVendettaSphereDuration -= Time.deltaTime;

        //fin de la protection, début de l'attaque de zone
        if (scrCompetenceManager.CompetenceManager.currentVendettaSphereDuration < 0)
        {

            scrCompetenceManager.CompetenceManager.vendettaSphereOn = false;
            scrCompetenceManager.CompetenceManager.currentVendettaSphereDuration = 0;
            PlayerRenderer.color = new Color(0f, 0.5f, 1f);
            ReflectRenderer.color = new Color(0f, 0.5f, 1f);
            scrCompetenceManager.CompetenceManager.vendettaSphereCurrentExpandDuration = scrCompetenceManager.CompetenceManager.vendettaSphereExpandDuration;
            scrCompetenceManager.CompetenceManager.vendettaSphereObject.transform.position = transform.position;

        }


    }

    void HeavenlyLightningUpdate()
    {

        if (scrCompetenceManager.CompetenceManager.currentGlobalCooldown == 0 && scrCompetenceManager.CompetenceManager.currentHeavenlyLightningCooldown <= 0 && ((scrCompetenceManager.CompetenceManager.slotOneName == scrCompetenceManager.CompetenceManager.heavenlyLightningName && Input.GetAxis("Slot One Input") != 0) || (scrCompetenceManager.CompetenceManager.slotTwoName == scrCompetenceManager.CompetenceManager.heavenlyLightningName && Input.GetAxis("Slot Two Input") != 0) || (scrCompetenceManager.CompetenceManager.slotThreeName == scrCompetenceManager.CompetenceManager.heavenlyLightningName && Input.GetAxis("Slot Three Input") != 0)))
        {

		

            scrCompetenceManager.CompetenceManager.currentHeavenlyLightningCooldown = scrCompetenceManager.CompetenceManager.heavenlyLightningCooldown;
            scrCompetenceManager.CompetenceManager.currentHeavenlyLightningExpandDuration = scrCompetenceManager.CompetenceManager.heavenlyLightningExpandDuration;
            scrCompetenceManager.CompetenceManager.heavenlyLightningObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, -10, 0);

        }

        if (scrCompetenceManager.CompetenceManager.currentHeavenlyLightningCooldown > 0)
            scrCompetenceManager.CompetenceManager.currentHeavenlyLightningCooldown -= Time.deltaTime;

        if (scrCompetenceManager.CompetenceManager.currentHeavenlyLightningCooldown < 0)
        {

            scrCompetenceManager.CompetenceManager.currentHeavenlyLightningCooldown = 0;


        }

    }

    void AnimationUpdate()
    {
        //on initialise la vitesse de l'animator
        PlayerAnimator.speed = 1;
        ReflectAnimator.speed = 1;

        #region animation de marche
        if (currentKnockbackForce <= 0 && currentPunchDuration <= 0)
        {
            //si le joueur n'est pas en train de marcher, alors...
            if (Body.velocity == Vector3.zero)
            {
                PlayerAnimator.SetBool("walking", false);
                ReflectAnimator.SetBool("walking", false);
            }

            //si le joueur est en train de marcher, alors...
            if (Body.velocity != Vector3.zero && currentPunchDuration <= 0 && currentStunDuration <= 0)
            {

                PlayerAnimator.SetBool("walking", true);
                ReflectAnimator.SetBool("walking", true);
                //il marche vers la gauche ou la droite
                if (Mathf.Abs(Body.velocity.x) > Mathf.Abs(Body.velocity.z))
                {

                    if (Body.velocity.x > 0)
                    {

                        PlayerAnimator.Play("marcheDroite");
                        ReflectAnimator.Play("marcheDroite");
                    }

                    if (Body.velocity.x < 0)
                    {

                        PlayerAnimator.Play("marcheGauche");
                        ReflectAnimator.Play("marcheGauche");
                    }

                }

                //il marche vers le haut ou le bas
                if (Mathf.Abs(Body.velocity.x) <= Mathf.Abs(Body.velocity.z))
                {

                    if (Body.velocity.z < 0)
                    {

                        PlayerAnimator.Play("marcheFace");
                        ReflectAnimator.Play("marcheFace");
                    }

                    if (Body.velocity.z > 0)
                    {

                        PlayerAnimator.Play("marcheDos");
                        ReflectAnimator.Play("marcheDos");
                    }

                }

            }

            //on change la vitesse selon si le joueur cours ou marche
            if (PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("marcheDroite") || PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("marcheGauche") || PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("marcheDos") || PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("marcheFace"))
            {

                //PlayerAnimator.speed = (speed / walkingSpeed) + 0.25f;

                if (speed == walkingSpeed && !scrExperienceManager.ExperienceManager.inCompetenceMenu)
                    PlayerAnimator.speed = 1.25f;
                if (speed == sprintSpeed && !scrExperienceManager.ExperienceManager.inCompetenceMenu)
                    PlayerAnimator.speed = 1.5f;


            }

            if (ReflectAnimator.GetCurrentAnimatorStateInfo(0).IsName("marcheDroite") || ReflectAnimator.GetCurrentAnimatorStateInfo(0).IsName("marcheGauche") || ReflectAnimator.GetCurrentAnimatorStateInfo(0).IsName("marcheDos") || ReflectAnimator.GetCurrentAnimatorStateInfo(0).IsName("marcheFace"))
            {

                //PlayerAnimator.speed = (speed / walkingSpeed) + 0.25f;

                if (speed == walkingSpeed && !scrExperienceManager.ExperienceManager.inCompetenceMenu)
                    ReflectAnimator.speed = 1.25f;
                if (speed == sprintSpeed && !scrExperienceManager.ExperienceManager.inCompetenceMenu)
                    ReflectAnimator.speed = 1.5f;


            }
        }
        #endregion

        #region attaque de base
        if (currentPunchDuration > 0 && currentStunDuration <= 0)
        {

            PlayerAnimator.SetBool("attacking", true);
            ReflectAnimator.SetBool("attacking", true);

            //on vérifie si le joueur va plus à l'horiontale ou à la verticale
            if (/*Mathf.Abs(Pivot.up.x) > Mathf.Abs(Pivot.up.z) &&*/ 1 == 1)
            {
                //droite ou gauche??
                if (Pivot.up.x > 0)
                {

                    PlayerAnimator.Play("sideBaseAttackRight");
                    ReflectAnimator.Play("sideBaseAttackRight");
                }
                if (Pivot.up.x < 0)
                {

                    PlayerAnimator.Play("sideBaseAttackLeft");
                    ReflectAnimator.Play("sideBaseAttackLeft");
                }

            }
            else
            {
                //haut ou bas??


            }
        }
        else
            PlayerAnimator.SetBool("attacking", false);
        ReflectAnimator.SetBool("attacking", false);
        #endregion

        #region stun
        //le joueur est il stun?
        if (currentKnockbackForce > 0)
        {

            PlayerAnimator.SetBool("stun", true);
            ReflectAnimator.SetBool("stun", true);
            //gauche - droite
            if (Mathf.Abs(currentKnockbackDirection.x) > Mathf.Abs(currentKnockbackDirection.z))
            {

                if (currentKnockbackDirection.x < 0)
                {
                    PlayerAnimator.Play("iwkaHurtLeft");
                    ReflectAnimator.Play("iwkaHurtLeft");
                }
                else
                {
                    PlayerAnimator.Play("iwkaHurtRight");
                    ReflectAnimator.Play("iwkaHurtRight");
                }

            }

            //haut - bas
            if (Mathf.Abs(currentKnockbackDirection.x) <= Mathf.Abs(currentKnockbackDirection.z))
            {

                if (currentKnockbackDirection.z < 0)
                {
                    PlayerAnimator.Play("iwkaHurtDown");
                    ReflectAnimator.Play("iwkaHurtDown");
                }
                else
                {
                    PlayerAnimator.Play("iwkaHurtUp");
                    ReflectAnimator.Play("iwkaHurtUp");

                }

            }

        }
        else
        {
            PlayerAnimator.SetBool("stun", false);
            ReflectAnimator.SetBool("stun", false);
        }
        #endregion

    }

    void DamageDesaturationUpdate()

    {


        if (life <= (maxLife) / 2.5f)
        {
            PPstackB.colorGrading.enabled = false;
            Debug.Log("Aiouuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuu");

            PPstack = mainCamera.GetComponent<PostProcessingBehaviour>().profile;
            PPstackB = CamB.GetComponent<PostProcessingBehaviour>().profile;

            PPstack.colorGrading.settings = colorSettings;
            

            PPstack.colorGrading.enabled = true;



            Satu = (life / 40)+0.3f;

			if (Satu <= 0.5f) 
			{
				Satu = 0.5f;
			}

            colorSettings.basic.saturation = Satu;



			mainCamera.gameObject.GetComponent<Klak.Motion.BrownianMotion> ().rotationFrequency = RotFreq;
			mainCamera.gameObject.GetComponent<Klak.Motion.BrownianMotion> ().rotationAmplitude = RotAmp;
			mainCamera.gameObject.GetComponent<Kino.Motion> ().shutterAngle = ShutAngle;
			mainCamera.gameObject.GetComponent<Kino.Motion> ().frameBlending = FrameBlending;

			RotFreq = (1 / life) + 0.05f;

			RotAmp = (1/life) + 0.02f;

			ShutAngle = 1500 * (1/life);
			FrameBlending =(1 - (1 / life))+0.1f;


            Debug.Log(colorSettings.basic.saturation);

            Debug.Log("Ouiaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");


			if (ShutAngle >= 180) 
			{
				ShutAngle = 180 ;
			}

			if (ShutAngle <= 60) 
			{
				ShutAngle = 0 ;
			}


			if (RotFreq >= 0.15f) 
			{
				RotFreq = 0.15f;
			}

			if (RotFreq >= 0.15f) 
			{
				RotFreq = 0.15f;
			}

			if (RotFreq <= 0.08f) 
			{
				RotFreq = 0;
			}

			if (RotAmp >= 0.11f) 
			{
				RotAmp = 0.11f;
			}

			if (RotAmp <= 0.08f) 
			{
				RotAmp = 0;
			}

			if (FrameBlending >= 0.5f) 
			{
				FrameBlending = 0.5f ;
			}

			if (FrameBlending <= 0.2f) 
			{
				FrameBlending = 0 ;
			}

        }
        else
        {
			mainCamera.gameObject.GetComponent<Klak.Motion.BrownianMotion> ().rotationFrequency = 0;
			mainCamera.gameObject.GetComponent<Klak.Motion.BrownianMotion> ().rotationAmplitude = 0;
			mainCamera.gameObject.GetComponent<Kino.Motion> ().shutterAngle = 0;
			mainCamera.gameObject.GetComponent<Kino.Motion> ().frameBlending = 0;
            Satu = 1.3f;
            PPstack.colorGrading.enabled = false;
            PPstackB.colorGrading.enabled = true;
		
        }

    }

}

