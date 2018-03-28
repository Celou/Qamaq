using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerLightning : MonoBehaviour {

    public float playerBulletSpeed;
    public bool isDestroyed;
    public Animator LightningAnimator;

	// Use this for initialization
	void Start () {

        isDestroyed = true;


    }
	
	// Update is called once per frame
	void Update () {

        if (scrExperienceManager.ExperienceManager.inCompetenceMenu == true)
            return;

        if (!LightningAnimator.GetCurrentAnimatorStateInfo(0).IsName("Destruction") && !LightningAnimator.GetCurrentAnimatorStateInfo(0).IsName("destroyed"))
            transform.Translate(Vector3.up * Time.deltaTime * playerBulletSpeed);

        if (isDestroyed == false && LightningAnimator.GetCurrentAnimatorStateInfo(0).IsName("destroyed"))
        {
            transform.position += new Vector3(0, -20, 0);
            LightningAnimator.SetBool("destroyed", false);

        }

        if (isDestroyed == true && LightningAnimator.GetCurrentAnimatorStateInfo(0).IsName("yamtaLightningAnim"))
        {

            LightningAnimator.SetBool("destroyed", true);

        }

        if (LightningAnimator.GetCurrentAnimatorStateInfo(0).IsName("destroyed"))
        {

            transform.position += new Vector3(0, 20, 0);

        }


    }
}
