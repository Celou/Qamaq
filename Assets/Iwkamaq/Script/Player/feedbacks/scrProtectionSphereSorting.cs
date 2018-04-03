using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrProtectionSphereSorting : MonoBehaviour {

    public SpriteRenderer PlayerRenderer;
    SpriteRenderer ProtectionSphereSprite;
    Animator ProtectionSphereAnimator;

	// Use this for initialization
	void Start () {

        ProtectionSphereSprite = GetComponent<SpriteRenderer>();
        ProtectionSphereAnimator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

        ProtectionSphereSprite.sortingOrder = PlayerRenderer.sortingOrder;

        /*if (scrCompetenceManager.CompetenceManager.protectionSphereOn)
        {

            ProtectionSphereAnimator.SetBool("protected", true);

        }
        else
        {

            ProtectionSphereAnimator.SetBool("protected", false);

        }*/

	}
}
