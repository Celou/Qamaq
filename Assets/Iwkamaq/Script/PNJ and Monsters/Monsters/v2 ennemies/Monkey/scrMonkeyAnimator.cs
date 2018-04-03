using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMonkeyAnimator : MonoBehaviour {

    [Header("Monkey Visuals")]
    Animator monkeyAnimator;
    SpriteRenderer monkeyRenderer;

    private void Start()
    {
        monkeyAnimator = GetComponent<Animator>();
        monkeyRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        AnimUpdate();
    }

    void AnimUpdate()
    {

        if (GetComponent<scrMonkeyBehavior>().attack)
        {
            monkeyAnimator.SetBool("preparing", false);
            monkeyAnimator.SetBool("attacking", true);
        }
        else
        {
            monkeyAnimator.SetBool("attacking", false);
            //if (chargingSound.isPlaying)
            //	chargingSound.Stop();
        }

        //idle
        if (!GetComponent<scrMonkeyBehavior>().attack && GetComponent<scrMonkeyBehavior>().currentCooldown > 0 && !GetComponent<scrMonkeyBehavior>().stun)
        {
            Debug.Log("idle");
            //droite ou gauche?
            if ((GetComponent<scrMonkeyBehavior>().Player.position - transform.position).x < 0)
            {
                //regarde à gauche
                monkeyRenderer.flipX = true;
            }
            else
            {
                //regarde à droite
                monkeyRenderer.flipX = false;
            }

            //haut ou bas?
            if ((GetComponent<scrMonkeyBehavior>().Player.position - transform.position).z < 0.5f)
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

        if (GetComponent<scrMonkeyBehavior>().preparing)
        {
            monkeyAnimator.SetBool("preparing", true);
        }

        //stun
        if (!GetComponent<scrMonkeyBehavior>().stunedToTheGround)
        {
            if (GetComponent<scrMonkeyBehavior>().stun)
                monkeyAnimator.SetBool("stun", true);
            else
                monkeyAnimator.SetBool("stun", false);
        }
        if (GetComponent<scrMonkeyBehavior>().stunedToTheGround)
        {

            monkeyAnimator.SetBool("bigStun", true);

            if (GetComponent<Rigidbody>().velocity.x > 0)
            {
                monkeyRenderer.flipX = true;
            }
            else
            {
                monkeyRenderer.flipX = false;
            }

        }
        else
            monkeyAnimator.SetBool("bigStun", false);
        //Debug.Log(currentStunDuration);
        //Debug.Log(currentStunDuration > 0);

    }

}
