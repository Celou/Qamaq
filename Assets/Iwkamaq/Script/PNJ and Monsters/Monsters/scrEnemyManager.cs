using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemyManager : MonoBehaviour {

    public static scrEnemyManager EnemyManager;

    [Header("BaseEnemies")]
    public GameObject[] BaseEnemiesArray;
    //public SpriteRenderer[] BaseEnemiesRendererArray;
    public float randomVariable;
    public float currentRandomVariable;
    public Transform enemyBaseTarget;
    public Transform Player;
    public int baseEnemyDamages;

    [Header("Gorilla Mini Boss")]
    public int gorillaRockDamage;
    public int gorillaContactDamage;
    public GameObject GorillaBossTarget;
    public Transform gorillaPivot;

    // Use this for initialization
    void Start () {

        EnemyManager = this;

	}
	
	// Update is called once per frame
	void Update () {

        if (scrCompetenceManager.CompetenceManager.taunted)
        {

            enemyBaseTarget = scrCompetenceManager.CompetenceManager.totemPosition;

        }
        else
        {

           enemyBaseTarget = Player;

        }
        
	}
}
