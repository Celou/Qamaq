using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemyBase : MonoBehaviour
{

	public Transform Player;

	[Header("Basic Properties")]
	public float attackCooldownMax;
	public float attackCooldownMin;
	public float attackDuration;
	float currentAttackDuration;
	float currentAttackCooldown;
	public bool attacking;
	public float attackSpeed;
	public Vector3 currentAttackDirection;
	//public int contactdamages;

	public bool isGoingToCharge;
	public float reactionTime;
	public float recoveringTime;
	public float recoveringTimeCurrent;
	public float life;
	public float seeingDistance;
	public bool seeingPlayer;
	public int experienceAmount;
	public Sprite idleSprite;
	Rigidbody body;
	bool collisioningPlayer;
	bool checkingForCollision;

	[Header("Stop")]
	bool stopped;
	Vector3 stockageDirection;

	[Header("Sprite")]
	public SpriteRenderer enemyBaseRenderer;
	public Animator monkeyAnimator;

	[Header("KnockBack")]
	public float knockbackSlowingDownVariable;
	float currentKnockbackForce;
	Vector3 currentKnockbackDirection;
	float takenKnockbackForce;

	[Header("Stun")]
	/*public Material normalMaterial;
    public Material stunMaterial;
    public Material flashRightMaterial;
    public Material flashLeftMaterial;*/
	public float flashDuration;
	public Sprite rightStunFlashSprite;
	public Sprite leftStunFlashSprite;
	public Sprite rightStunSprite;
	public Sprite leftStunSprite;
	float currentFlashDuration;
	bool stun;
	bool bigStun;
	float currentStunDuration;
	public GameObject hitParticles;

	[Header("Death")]
	public GameObject deathParticles;
	//public GameObject deathParticlesGamma;

	[Header("Sounds")]
	public AudioSource chargePreparationSound;
	public AudioSource chargingSound;

	public AudioSource hurtSound1;
	public AudioSource hurtSound2;
	public AudioSource hurtSound3;

	public AudioSource screamSound1;
	public AudioSource screamSound2;
	public AudioSource screamSound3;

	public AudioSource deathSound;


	void Start()
	{
		currentAttackCooldown = Random.Range(attackCooldownMin, attackCooldownMax) ;
		body = GetComponent<Rigidbody>();
		Player = GameObject.Find("Perso").transform;
		chargePreparationSound = GetComponent<AudioSource>();
		chargingSound = GetComponent<AudioSource>();
		hurtSound1 = GetComponent<AudioSource>();
		hurtSound2 = GetComponent<AudioSource>();
		hurtSound3 = GetComponent<AudioSource>();
		screamSound1 = GetComponent<AudioSource>();
		screamSound2 = GetComponent<AudioSource>();
		screamSound3 = GetComponent<AudioSource>();
		deathSound = GetComponent<AudioSource>();
	}

	void Update()
	{

		if (life <= 0)
			monkeyAnimator.speed = 0.01f;

		if (scrExperienceManager.ExperienceManager.inCompetenceMenu == true)
		{
			if (!stopped)
			{
				stopped = true;
				stockageDirection = body.velocity;
				body.velocity = Vector3.zero;
				monkeyAnimator.speed = 0;
			}
			return;
		}
		else
		{
			if (stopped)
			{
				stopped = false;
				body.velocity = stockageDirection;
				monkeyAnimator.speed = 1;
			}
		}

		if (attacking)
		{

			Attack();

		}

		StunUpdate();

		AnimUpdate();

		KnockBackUpdate();

        if (Vector3.Distance(scrEnemyManager.EnemyManager.enemyBaseTarget.position, transform.position) > seeingDistance && scrClearingEventManager.ClearingEventManager.eventing == false)
			return;

		if (stun)
			return;

		if (Vector3.Distance(Player.position, transform.position) < seeingDistance && scrTutorialTextManager.TutorialTextManager.enemyEncounter == false)
		{

			scrTutorialTextManager.TutorialTextManager.tutoDialogueCurrentLine = 3;
			scrTutorialTextManager.TutorialTextManager.enemyEncounter = true;

		}

		Recovering();

		/*if (life <= 0)
        {
            scrExperienceManager.ExperienceManager.currentExperience += experienceAmount;
            Instantiate(deathParticles, transform.position, transform.rotation);
            Destroy(enemyBaseRenderer);            
            Destroy(gameObject);            
        }*/

		AttackUpdate();

		//enemyBaseRenderer.transform.rotation = Quaternion.Euler(90, 0, 0);

		if(currentKnockbackForce <= 0 && !attacking)
			body.velocity = Vector3.zero;


	}

	void Aim()
	{

		Vector3 targetPos = scrEnemyManager.EnemyManager.enemyBaseTarget.position;
		targetPos.y = 0;
		Vector3 aim = targetPos - transform.position;

		float rotY = Mathf.Atan2(aim.x, aim.z) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler(90, rotY, 0);

	}

	void AttackUpdate()
	{
		checkingForCollision = false;

		currentAttackCooldown -= Time.deltaTime;

		if (currentAttackDuration > 0)
			currentAttackDuration -= Time.deltaTime;

		if (currentAttackDuration < 0)
		{
			currentAttackDuration = 0;
			body.velocity = Vector3.zero;
			GetComponent<BoxCollider>().enabled = true;
			checkingForCollision = true;
		}

		if (currentAttackDuration == 0)
			attacking = false;

		if (currentAttackCooldown <= 0)
		{
			currentAttackCooldown = Random.Range(attackCooldownMin, attackCooldownMax);
			attacking = true;
			currentAttackDuration = attackDuration;
			currentAttackDirection = (scrEnemyManager.EnemyManager.enemyBaseTarget.position - transform.position).normalized;

		}

		/*if (currentAttackCooldown > reactionTime && attacking == false)
            enemyBaseRenderer.color = new Color(1, 1, 1, 1);

        if (currentAttackCooldown < reactionTime)
            enemyBaseRenderer.color = new Color(1, 0.5f, 0.5f, 1);*/


	}

	void Attack()
	{

		//transform.Translate(currentAttackDirection * attackSpeed * Time.deltaTime, Space.World);
		body.velocity = currentAttackDirection * attackSpeed * Time.deltaTime;

	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player" && checkingForCollision)
		{
			collisioningPlayer = true;
			GetComponent<BoxCollider>().enabled = false;

		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (collisioningPlayer == true)
		{
			collisioningPlayer = false;
			GetComponent<BoxCollider>().enabled = true;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (currentFlashDuration <= 0)
		{

			#region attaque du joueur

			//on prend les dégats, la force de knockback et la durée du stun
			float damagesTaken = scrDamagesManager.DamagesManager.GetPlayerDamages(other.tag);
			float knockbackForceTaken = scrDamagesManager.DamagesManager.GetPlayerKnockBackForce(other.tag);
			float stunDurationTaken = scrDamagesManager.DamagesManager.GetPlayerStunDuration(other.tag);
			int randomHurtSoundVariable = Random.Range(0, 3);

			#region degats

			//dégats
			if (damagesTaken != 0)
			{

				life -= damagesTaken;
				recoveringTimeCurrent = recoveringTime;

				if (randomHurtSoundVariable == 0)
					hurtSound1.Play();

				if (randomHurtSoundVariable == 1)
					hurtSound2.Play();

				if (randomHurtSoundVariable == 2)
					hurtSound3.Play();

				if (life <= 0)
					deathSound.Play();
			}
			#endregion

			#region knockback
			//knockback
			if (knockbackForceTaken != 0 && damagesTaken != 0)
			{
				currentKnockbackForce = knockbackForceTaken;

				if (other.tag == "playerPunch")
				{
					Instantiate(hitParticles, transform.position, new Quaternion(-other.transform.rotation.x, other.transform.rotation.y, -other.transform.rotation.z, other.transform.rotation.w));

					//currentKnockbackDirection = Player.GetComponent<scrPlayer>().Pivot.up;

				}

				if (other.tag == "playerLightning")
				{
					currentKnockbackDirection = other.GetComponent<scrPlayerLightning>().transform.up;
				}

				if (other.tag == "heavenlyLightning")
				{
					currentKnockbackDirection = (transform.position - other.transform.position).normalized;
				}

				if (other.tag == "vendettaSphere")
				{
					currentKnockbackDirection = (transform.position - other.transform.position).normalized;
				}

				currentKnockbackDirection = new Vector3(currentKnockbackDirection.x, 0, currentKnockbackDirection.z);

				takenKnockbackForce = knockbackForceTaken;
			}
			#endregion

			#region stun
			//stun
			if (stunDurationTaken != 0 && damagesTaken != 0)
			{
				stun = true;

				if (attacking)
					bigStun = true;
				else
					bigStun = false;

				if (attacking)
					currentStunDuration = stunDurationTaken * 5;
				else
					currentStunDuration = stunDurationTaken;

				currentFlashDuration = flashDuration;

				if (attacking)
					attacking = false;
			}

			if (life <= 0)
			{
				if (other.tag == "playerPunch")
				{

					Player.GetComponent<scrPlayer>().mainCamera.GetComponent<Screenshake>().shakeDuration = Player.GetComponent<scrPlayerPunchUpdate>().finalPunchScreenshakeDuration;
					Player.GetComponent<scrPlayer>().mainCamera.GetComponent<Screenshake>().shakeAmount = Player.GetComponent<scrPlayerPunchUpdate>().finalPunchScreenshakeAmount;
				}

			}
			#endregion

			#region autres
			if (other.tag == "playerLightning")
			{
				other.GetComponent<scrPlayerLightning>().isDestroyed = true;
			}
			#endregion

			#endregion
		}

		#region sphere de vendetta
		/*if (other.tag == "vendettaSphere" /*&& scrEffectZoneManager.effectZoneManager.effectZoneMode == "Invincibility"*/ /*)
		{

			life -= scrCompetenceManager.CompetenceManager.vendattaShereDamageStockage;

			if (scrCompetenceManager.CompetenceManager.vendattaShereDamageStockage > 0)
				recoveringTimeCurrent = recoveringTime;

		}*/
		#endregion

		#region eclair celeste
		/*if (other.tag == "heavenlyLightning")
        {

            life -= scrCompetenceManager.CompetenceManager.heavenlyLightningDamage;

            if (scrCompetenceManager.CompetenceManager.heavenlyLightningDamage > 0)
                recoveringTimeCurrent = recoveringTime;

        }*/
		#endregion


		if (other.tag == "limite_haut" || other.tag == "limite_bas" || other.tag == "limite_droite" || other.tag == "limite_gauche")
			attacking = false;
	}

	void Recovering() {

		if (recoveringTimeCurrent > 0)
			recoveringTimeCurrent-=Time.deltaTime;

		if (recoveringTimeCurrent < 0)
			recoveringTimeCurrent = 0;

	}

	/*IEnumerator KnockBack(float knockBackForce, Vector3 knockBackDirection)
    {
        while (knockBackForce > 0)
        {
            //fait baisser le knockback
            knockBackForce -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
            transform.Translate(knockBackDirection * knockBackForce, Space.World);

        }
    }*/

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
			body.velocity = 4 * currentKnockbackDirection * currentKnockbackForce * Time.deltaTime;

			//on fait diminuer la force du Knockback
			currentKnockbackForce -= knockbackSlowingDownVariable * Time.deltaTime;

		}

	}

	void StunUpdate()
	{
		if (currentFlashDuration > 0)
		{
			currentFlashDuration -= Time.deltaTime;


			if(currentKnockbackDirection.x < 0)
			{
				enemyBaseRenderer.sprite = leftStunFlashSprite;
				enemyBaseRenderer.flipX = false;

			}
			else
			{
				enemyBaseRenderer.sprite = rightStunFlashSprite;
				enemyBaseRenderer.flipX = false;

			}
		}
		if(currentFlashDuration < 0)
		{
			//mort de l'ennemi
			if(life <= 0)
			{
				scrExperienceManager.ExperienceManager.currentExperience += experienceAmount;
				//Instantiate(deathParticlesGamma, transform.position, transform.rotation);
				Instantiate(deathParticles, transform.position, new Quaternion(-transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));
				Destroy(enemyBaseRenderer);
				Destroy(gameObject);
			}

			currentFlashDuration = 0;

		}

		if (currentStunDuration > 0)
		{
			currentStunDuration -= Time.deltaTime;
			//Debug.Log("tamer");

		}
		if (currentStunDuration < 0)
		{
			currentStunDuration = 0;
			stun = false;
			bigStun = false;
		}

	}

	void AnimUpdate()
	{
		//idle
		if (!attacking && currentAttackCooldown > 0 && !stun)
		{
			//droite ou gauche?
			if ((scrEnemyManager.EnemyManager.enemyBaseTarget.position - transform.position).x < 0)
			{
				//regarde à gauche
				enemyBaseRenderer.flipX = true;
			}
			else
			{
				//regarde à droite
				enemyBaseRenderer.flipX = false;
			}

			//haut ou bas?
			if ((scrEnemyManager.EnemyManager.enemyBaseTarget.position - transform.position).z < 0.5f)
			{
				//regarde en bas (de face)
				monkeyAnimator.SetBool("face", true);
			}
			else
			{
				//regarde en haut (de dos)
				monkeyAnimator.SetBool("face", false);
			}
		}

		if (currentAttackCooldown < reactionTime)
		{
			monkeyAnimator.SetBool("preparing", true);
			if (!chargePreparationSound.isPlaying)
				chargePreparationSound.Play();
		}

		if (attacking)
		{
			monkeyAnimator.SetBool("preparing", false);
			monkeyAnimator.SetBool("attacking", true);
			if (chargePreparationSound.isPlaying)
				chargePreparationSound.Stop();
			if (!chargingSound.isPlaying)
				chargingSound.Play();
		}
		else
		{
			monkeyAnimator.SetBool("attacking", false);
			//if (chargingSound.isPlaying)
			//	chargingSound.Stop();
		}

		//stun
		if (!bigStun)
		{            
			if (currentStunDuration > 0)
				monkeyAnimator.SetBool("stun", true);
			else
				monkeyAnimator.SetBool("stun", false);
		}
		if (bigStun)
		{

			monkeyAnimator.SetBool("bigStun", true);

			if(currentKnockbackDirection.x > 0)
			{
				enemyBaseRenderer.flipX = true;
			}
			else
			{
				enemyBaseRenderer.flipX = false;
			}

		}
		else
			monkeyAnimator.SetBool("bigStun", false);
		//Debug.Log(currentStunDuration);
		//Debug.Log(currentStunDuration > 0);

	}



}