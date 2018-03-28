using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrTauntingSummon : MonoBehaviour {

    Animator TotemAnimator;
	public float life;
	public float recoveringTime;
	public float recoveringTimeCurrent;
	public bool recovering;

	void Start () {

        TotemAnimator = GetComponent<Animator>();
		life = 100;
		recoveringTime = 0.2f;
    }


	void Update () {

		Recovering ();

        if (scrExperienceManager.ExperienceManager.inCompetenceMenu == true)
            return;

        if (scrCompetenceManager.CompetenceManager.totemSummoned == false)
        {

            transform.position = scrCompetenceManager.CompetenceManager.totemPosition.position;

        }

        if (scrCompetenceManager.CompetenceManager.currentTotemLife <= 0)
        {

            scrCompetenceManager.CompetenceManager.currentTotemLife = 0;
            scrCompetenceManager.CompetenceManager.totemSummoned = false;
            scrCompetenceManager.CompetenceManager.currentTotemDuration = 0;
            
        }

        if (scrCompetenceManager.CompetenceManager.totemSummoned)
        {

            TotemAnimator.SetBool("invoked", true);

        }
        else
        {

            TotemAnimator.SetBool("invoked", false);

        }


    }

	void Recovering() 
	{
		if (recovering == true)
			recoveringTimeCurrent -= Time.deltaTime;

		if (recoveringTimeCurrent < 0)
			recoveringTimeCurrent = 0;

		if (recoveringTimeCurrent == 0)
			recovering = false;
	}

	void OnTriggerEnter(Collider other)
	{
		float damagesTaken = scrDamagesManager.DamagesManager.GetEnemyDamages(other.tag);

		bool enemyAttacking = false;
		if (other.tag == "monkey")
			enemyAttacking = other.GetComponent<scrEnemyBase>().attacking;
		if (other.tag == "crawler")
			enemyAttacking = true;

		if (damagesTaken != 0) {

			if (other.tag == "enemy1" || other.tag == "enemy3")
				enemyAttacking = true;

			if (recovering == false)
			{
				life -= damagesTaken;
				recovering = true;
				recoveringTimeCurrent = recoveringTime;
			}

			if (life <= 0) {
				Destroy (gameObject);

				if (recovering == true) {
					return;
				}
			}
		} 
	}



    
}
