using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBoostSorting : MonoBehaviour {

    public SpriteRenderer PlayerRenderer;
    SpriteRenderer BoostSprite;
    Animator BoostAnimator;

	// Use this for initialization
	void Start () {

        BoostSprite = GetComponent<SpriteRenderer>();
        BoostAnimator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

        BoostSprite.sortingOrder = PlayerRenderer.sortingOrder;

        if (scrCompetenceManager.CompetenceManager.currentBoostDuration > 0)
        {

            BoostAnimator.SetBool("boosted", true);

        }
        else
        {

            BoostAnimator.SetBool("boosted", false);

        }

	}
}
