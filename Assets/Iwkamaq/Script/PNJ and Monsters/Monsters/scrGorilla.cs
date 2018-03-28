using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrGorilla : MonoBehaviour {

    [Header("Basical Parameters")]
    public float life;
    public int phase;
    public float baseSpeed;
    float currentSpeed;
    public SpriteRenderer GorillaRenderer;
    public int GorillaPosition;
    public Transform Pivot;
    public Camera Camera;

    [Header("Attack Parameters")]
    public float seeingDistance;
    public bool isAttacking;
    public GameObject BigRock;
    public GameObject Player;
    public GameObject TauntingSummon;
    public float minAttackCooldown;
    public float maxAttackCooldown;
    float currentAttackCooldown;
    public float minPickUpCooldown;
    public float maxPickUpCooldown;
    float currentPickUpCooldown;
    bool isHoldingRock;
    public GameObject contactZoneAttack;

    

    // Use this for initialization
    void Start () {

        currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
        currentPickUpCooldown = Random.Range(minPickUpCooldown, maxPickUpCooldown);

    }
	
	// Update is called once per frame
	void Update () {

        if (life <= 0)
        {

            scrTutorialTextManager.TutorialTextManager.tutoDialogueCurrentLine = 9;
            scrTutorialTextManager.TutorialTextManager.tutoEnded = true;
            Destroy(gameObject);

        }

        if (scrExperienceManager.ExperienceManager.inCompetenceMenu)
            return;

        if (isAttacking)
        {

            if(scrTutorialTextManager.TutorialTextManager.gorillaEncounter == false)
            {

                scrTutorialTextManager.TutorialTextManager.tutoDialogueCurrentLine = 7;
                scrTutorialTextManager.TutorialTextManager.gorillaEncounter = true;

            }

            Aim();
            AttackUpdate();

        }

        if (Vector3.Distance(transform.position, Player.transform.position) < seeingDistance)
        {
            //Camera.orthographicSize = seeingDistance/2;
            isAttacking = true;

        }
        else
        {

            //Camera.orthographicSize = 8.1f;
            isAttacking = false;

        }

	}

    void Aim()
    {

        if (scrCompetenceManager.CompetenceManager.taunted)
            scrEnemyManager.EnemyManager.GorillaBossTarget = TauntingSummon;
        else
            scrEnemyManager.EnemyManager.GorillaBossTarget = Player;

        Vector3 targetPos;

        targetPos = scrEnemyManager.EnemyManager.GorillaBossTarget.transform.position;
        targetPos.y = 0;
        Vector3 dir = targetPos - transform.position;

        float rotY = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        Pivot.rotation = Quaternion.Euler(90, rotY, 0);
        scrEnemyManager.EnemyManager.gorillaPivot.rotation = Pivot.rotation;

    }

    void AttackUpdate()
    {

        if (currentPickUpCooldown > 0)
            currentPickUpCooldown -= Time.deltaTime;

        if (currentPickUpCooldown <= 0)
        {

            isHoldingRock = true;
            GorillaRenderer.color = new Color(1, 0, 0);
            
        }

        if (currentAttackCooldown > 0 && isHoldingRock)
        {
            if (scrTutorialTextManager.TutorialTextManager.gorillaAttacked == false)
            {

                scrTutorialTextManager.TutorialTextManager.tutoDialogueCurrentLine++;
                scrTutorialTextManager.TutorialTextManager.gorillaAttacked = true;

            }

            currentAttackCooldown -= Time.deltaTime;
            BigRock.transform.position = Pivot.transform.position;

        }

        if (currentAttackCooldown <= 0 /*&& Vector3.Distance(Player.transform.position, transform.position) > 5*/)
        {

            currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
            currentPickUpCooldown = Random.Range(minPickUpCooldown, maxPickUpCooldown);
            GorillaRenderer.color = new Color(1, 1, 1);
            isHoldingRock = false;
            BigRock.GetComponent<scrGorillaRock>().enabled = true;
            BigRock.transform.rotation = Pivot.transform.rotation;

        }

        /*if (currentAttackCooldown <= 0 && Vector3.Distance(Player.transform.position, transform.position) < 5)
        {

            currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
            currentPickUpCooldown = Random.Range(minPickUpCooldown, maxPickUpCooldown);
            GorillaRenderer.color = new Color(1, 1, 1);
            isHoldingRock = false;
            contactZoneAttack.transform.localScale = new Vector3(8, 8, 8);

        }*/

    }

    /*void OnTriggerEnter(Collider other)
    {

        if (other.tag == "playerPunch")
        {

            life -= Player.GetComponent<scrPlayer>().punchDamage;

        }

        if (other.tag == "player_bullet")
        {

            life -= Player.GetComponent<scrPlayer>().bulletDamage;

        }


    }*/

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "playerPunch")
        {

            life -= Player.GetComponent<scrPlayer>().currentPlayerDamage;
            

        }

        if (other.tag == "vendettaSphere" /*&& scrEffectZoneManager.effectZoneManager.effectZoneMode == "Invincibility"*/)
        {
            Debug.Log("zone");
            life -= scrCompetenceManager.CompetenceManager.vendattaShereDamageStockage;

        }

        if (other.tag == "player_bullet")
        {

            Debug.Log("eclair");
            life -= scrCompetenceManager.CompetenceManager.yamtaLightningDamage;
            other.gameObject.transform.position += new Vector3(0, 20, 0);

        }

    }

    void Attack()
    {



    }


}
