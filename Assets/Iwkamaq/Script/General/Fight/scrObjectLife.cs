using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(scrPlayer))]
public class scrObjectLife : MonoBehaviour {

    [Header("Life")]
    public float maxLife;
    public float currentLife;
    bool dead;

    [Header("Recover")]
    public float recoveringTime;
    public float currentRecoveringTime;

    [Header("Experience")]
    public int enemyExperience;

	// Use this for initialization
	void Start () {
        currentLife = maxLife;

        currentRecoveringTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if(currentLife <= 0 && !dead) 
        {
            StartCoroutine(Death());
        }

        RecoveringUpdate();
    }

    void RecoveringUpdate()
    {
        if (currentRecoveringTime > 0)
            currentRecoveringTime -= Time.deltaTime;

        if (currentRecoveringTime < 0)
            currentRecoveringTime = 0;
    }

    IEnumerator Death()
    {
        dead = true;

        yield return new WaitForSeconds(0.5f);

        scrPlayerExperience.PlayerExperience.GainExperience(enemyExperience);

        Destroy(transform.parent.gameObject);
    }
}
