using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrInsectBehavior : MonoBehaviour
{

	public Transform Player;
	public GameObject Crawler;
	//public Transform CrawlerTransform;

	[Header("Basic Properties")]
	public float contactdamages;
	public float seeingDistance;
	public int experienceAmount;
	public float speed;
	Vector3 direction;
	Vector3 direction2;
	Rigidbody body;
	private float bounciness;
	public bool swarm;


	void Start()
	{

		Player = GameObject.Find("Perso").transform;
		body = GetComponent<Rigidbody>();
		swarm = false;
		//Crawler = GameObject.FindWithTag("crawler");
		Crawler = gameObject;

	}

	void Update()
	{
		bounciness = 0;
		body.velocity = new Vector3( 0,0,0 );
		body.velocity = Vector3.zero;
		speed = 200;

		if (scrExperienceManager.ExperienceManager.inCompetenceMenu == true)
			return;

		if (Vector3.Distance (Player.position, transform.position) > seeingDistance)
			return;
		if (swarm == false) {
			direction = Player.position - transform.position;
		}

		if (swarm == true) {
			direction = Player.position - transform.position;
			speed = Random.Range (100, 150);
		}

		Attack();
		if (Player.position.x > transform.position.x)
			GetComponent<SpriteRenderer>().flipX = false;
		else
			GetComponent<SpriteRenderer>().flipX = true;
	}



	void Attack()
	{
		body.velocity = direction.normalized * speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "crawler") {
			swarm = true;
		}

		if (other.tag == "Player") {
			body.isKinematic = true;
		}
			float damagesTaken = scrDamagesManager.DamagesManager.GetPlayerDamages(other.tag);

			if (damagesTaken != 0)
			{
				Destroy(gameObject);
				scrExperienceManager.ExperienceManager.currentExperience += experienceAmount;
			}

		}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player") {
			body.isKinematic = false;

			if (other.tag == "crawler") {
				swarm = false;
			}
		}

	}
}