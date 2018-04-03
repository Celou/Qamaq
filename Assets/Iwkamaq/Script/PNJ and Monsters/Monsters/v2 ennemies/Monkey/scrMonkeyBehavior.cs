using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMonkeyBehavior : MonoBehaviour {

    [Header("States")]
	public bool idle;
	public bool walk;
	public bool chase;
	public bool detect;
	public bool attack;

    [Header("Physic")]
	Rigidbody body;
	Vector3 direction;
	Vector3 attackdirection;

    [Header("Attack")]
	public float detectDistance;
	public float speed;
	public Transform Player;
	public float distance;
	public float attackDistance;
	//public Vector3[] randomDirection;
	public Vector3 randomDirection;
	public Vector3 currentAttackDirection;
	public float attackSpeed;
	public float currentCooldown;
	public float Cooldownmin;
	public float Cooldownmax;
	public float currentAttackDuration;
	public float attackDuration;

    [Header("Attack Values")]
    public float monkeyDamages;
    public float monkeyKnockBackForce;
    public float monkeyStunDuration;

    [Header("Vulnerability")]
    public float monkeyVulnerabilityMultiplicator;

    [Header("Stun")]
    bool stun;

	//Création 26 mars 2018
	// Modification 28 mars 2018 Ajout ChaseState => Working \\ Objectif => Attack State, Fleeing State, Special Attack State


	//Initialize
	void Awake(){
		body = GetComponent<Rigidbody>();
		Player = GameObject.Find("Perso").transform;
		walk = false;
		idle = false;
		speed = 75;
		detectDistance = 12;
		distance = 25;
		attackDistance = 8;
		currentCooldown = 0;
		Cooldownmin = 2;
		Cooldownmax = 4;
		attackDuration = 0.5f;
	}
	void Start(){
		StartCoroutine (IdleRoutine());

	}

	void Update(){
		StartCoroutine (DetectRoutine ());
		StartCoroutine (ChaseRoutine ());
		StartCoroutine (AttackRoutine ());
        StartCoroutine(StunRoutine());
        
		AttackUpdate ();
        VulnerabilityUpdate();
        StunUpdate();

        if (currentCooldown <= 0){
			currentCooldown = 0;
		}
		if (currentCooldown > 0){
			currentCooldown -= Time.deltaTime;
		}        
	}

	void FixedUpdate(){
		direction = Player.position - transform.position;
	}

	//IdleState
	private IEnumerator IdleRoutine()
	{
		randomDirection = new Vector3 (Random.Range (1f, -1f), 0, Random.Range (1f, -1f));
		idle = true;
		walk = false;

        if(!stun)
		    body.velocity = direction.normalized * 0 * Time.deltaTime;

		yield return new WaitForSeconds(Random.Range(2,7));
		StartCoroutine (WalkRoutine ());
	}

	//WalkingState
	private IEnumerator WalkRoutine()
	{
		if (Vector3.Distance(Player.position, transform.position) > distance) {
			body.velocity = direction.normalized * speed * Time.deltaTime;
		} else {
			body.velocity = randomDirection.normalized * speed * Time.deltaTime;
		}
		walk = true;
		idle = false;
		yield return new WaitForSeconds(Random.Range(2,7));
		StartCoroutine(IdleRoutine());
	}

	//DetectState
	private IEnumerator DetectRoutine(){
		if (Vector3.Distance (Player.position, transform.position) < detectDistance) {
			detect = true;
		}
		else{
			detect = false;
		}
		yield return null;

	}

	//ChaseState
	private IEnumerator ChaseRoutine(){
		if (Vector3.Distance (Player.position, transform.position) > attackDistance && detect == true) {
			chase = true;
			body.velocity = direction.normalized * speed * Time.deltaTime;
		}
		yield return new WaitForSeconds (100f);

	}
	//AttackState
	private IEnumerator AttackRoutine(){
		if (Vector3.Distance (Player.position, transform.position) <= attackDistance && currentCooldown == 0 && !stun) {
			body.velocity = direction.normalized * 0 * Time.deltaTime;
			attackdirection = Player.position - transform.position;
			yield return new WaitForSeconds (1f);
			body.velocity = attackdirection.normalized * 1000 * Time.deltaTime;
			currentCooldown = Random.Range (Cooldownmin, Cooldownmax);
			if (currentAttackDuration <= 0) {
				body.velocity = Vector3.zero;
			} else {
				attack = true;
			}
		}
        if (currentAttackDuration <= 0)
            attack = false;
    }

    //Stun State
    IEnumerator StunRoutine()
    {
        if (stun)
            body.velocity = Vector3.zero;
        yield return null;
    }

    void AttackUpdate()
	{
		if (currentAttackDuration > 0)
			currentAttackDuration -= Time.deltaTime;
		
		if (currentAttackDuration < 0)
		{
			currentAttackDuration = 0;
		}

		if (currentCooldown <= 0)
		{
			currentAttackDuration = attackDuration;
		}

        if (attack)
        {
            gameObject.layer = 20;
            /*if(GetComponent<BoxCollider>().enabled == true)
                GetComponent<BoxCollider>().enabled = false;*/
        }
        else
        {
            gameObject.layer = 17;
            /*if (GetComponent<BoxCollider>().enabled == false)
                GetComponent<BoxCollider>().enabled = true;*/
        }
	}

    void VulnerabilityUpdate()
    {
        GetComponentInChildren<scrObjectStun>().vulnerable = attack;
        GetComponentInChildren<scrObjectStun>().vulnerabilityMultiplicator = monkeyVulnerabilityMultiplicator;
    }

    void StunUpdate()
    {
        stun = (GetComponentInChildren<scrObjectStun>().currentStunDuration > 0);
    }

}

