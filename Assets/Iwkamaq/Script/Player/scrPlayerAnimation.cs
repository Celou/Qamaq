using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerAnimation : MonoBehaviour {

    [Header("Player Properties")]
    Rigidbody playerBody;
    Transform pivot;

    [Header("Player Animator")]
    Animator playerAnimator;
    SpriteRenderer playerRenderer;

    [Header("Player Reflect")]
    public SpriteRenderer reflectRenderer;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
        playerBody = GetComponentInParent<Rigidbody>();
        pivot = GetComponentInParent<scrPlayerLookingDirection>().pivot;
    }

    private void Update()
    {
        InCompetenceMenu();

        ReflectUpdate();

        if(!GetComponentInParent<scrPlayerPaused>().playerPaused)
            AnimationUpdate();
    }

    void ReflectUpdate()
    {
        reflectRenderer.sprite = playerRenderer.sprite;
    }

    void AnimationUpdate()
    {
        //on initialise la vitesse de l'animator
        playerAnimator.speed = 1;

        #region animation de marche
        if (!GetComponentInParent<scrPlayerPunchUpdate>().punching && !GetComponentInParent<scrDash>().dashing && !GetComponentInParent<scrPlayerKnockBack>().knockbacked)
        {
            //si le joueur n'est pas en train de marcher, alors...
            if (playerBody.velocity == Vector3.zero && (!Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.Q))) ;
            {
                playerAnimator.SetBool("walking", false);
            }
            
            //si le joueur est en train de marcher, alors...
            if (playerBody.velocity != Vector3.zero && (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Q)))
            {
                playerAnimator.SetBool("walking", true);
                //il marche vers la gauche ou la droite
                if (Mathf.Abs(playerBody.velocity.x) > Mathf.Abs(playerBody.velocity.z))
                {

                    if (playerBody.velocity.x > 0)
                    {

                        playerAnimator.Play("marcheDroite");
                    }

                    if (playerBody.velocity.x < 0)
                    {

                        playerAnimator.Play("marcheGauche");
                    }

                }

                //il marche vers le haut ou le bas
                if (Mathf.Abs(playerBody.velocity.x) <= Mathf.Abs(playerBody.velocity.z))
                {

                    if (playerBody.velocity.z < 0)
                    {
                        playerAnimator.Play("marcheFace");
                    }

                    if (playerBody.velocity.z > 0)
                    {

                        playerAnimator.Play("marcheDos");
                    }

                }

            }

            //on change la vitesse selon si le joueur cours ou marche
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("marcheDroite") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("marcheGauche") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("marcheDos") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("marcheFace"))
            {

                //PlayerAnimator.speed = (speed / walkingSpeed) + 0.25f;

                if (GetComponentInParent<scrPlayerMovementUpdate>().currentSpeed == GetComponentInParent<scrPlayerMovementUpdate>().walkingSpeed && !scrCompetenceMenu.CompetenceMenu.inCompetenceMenu)
                    playerAnimator.speed = 1.25f;
                if (GetComponentInParent<scrPlayerMovementUpdate>().currentSpeed == GetComponentInParent<scrPlayerMovementUpdate>().sprintSpeed && !scrCompetenceMenu.CompetenceMenu.inCompetenceMenu)
                    playerAnimator.speed = 1.5f;


            }

        }
        #endregion

        #region attaque de base
        if (GetComponentInParent<scrPlayerPunchUpdate>().punching)
        {

            playerAnimator.SetBool("attacking", true);

            //on vérifie si le joueur va plus à l'horiontale ou à la verticale
            if (/*Mathf.Abs(Pivot.up.x) > Mathf.Abs(Pivot.up.z) &&*/ 1 == 1)
            {
                //droite ou gauche??
                if (pivot.up.x > 0)
                {
                    if(GetComponentInParent<scrPlayerPunchUpdate>().currentPunchCombo == 1)
                        playerAnimator.Play("sideBaseAttackRight 1");

                    if (GetComponentInParent<scrPlayerPunchUpdate>().currentPunchCombo == 2)
                        playerAnimator.Play("sideBaseAttackRight 2");

                    if (GetComponentInParent<scrPlayerPunchUpdate>().currentPunchCombo == 3)
                        playerAnimator.Play("sideBaseAttackRight 3");

                    if (GetComponentInParent<scrPlayerPunchUpdate>().currentPunchCombo == 0)
                        playerAnimator.Play("sideBaseAttackRight 4");
                }
                if (pivot.up.x < 0)
                {
                    if (GetComponentInParent<scrPlayerPunchUpdate>().currentPunchCombo == 1)
                        playerAnimator.Play("sideBaseAttackLeft 1");

                    if (GetComponentInParent<scrPlayerPunchUpdate>().currentPunchCombo == 2)
                        playerAnimator.Play("sideBaseAttackLeft 2");

                    if (GetComponentInParent<scrPlayerPunchUpdate>().currentPunchCombo == 3)
                        playerAnimator.Play("sideBaseAttackLeft 3");

                    if (GetComponentInParent<scrPlayerPunchUpdate>().currentPunchCombo == 0)
                        playerAnimator.Play("sideBaseAttackLeft 4");
                }

            }
            else
            {
                //haut ou bas??


            }
        }
        else
            playerAnimator.SetBool("attacking", false);
        #endregion

        #region stun
        //le joueur est il stun?
        if (GetComponentInParent<scrPlayerKnockBack>().knockbacked)
        {

            playerAnimator.SetBool("stun", true);
            //gauche - droite
            if (Mathf.Abs(GetComponentInParent<scrPlayerKnockBack>().currentKnockbackDirection.x) > Mathf.Abs(GetComponentInParent<scrPlayerKnockBack>().currentKnockbackDirection.z))
            {

                if (GetComponentInParent<scrPlayerKnockBack>().currentKnockbackDirection.x < 0)
                {
                    playerAnimator.Play("iwkaHurtLeft");
                }
                else
                {
                    playerAnimator.Play("iwkaHurtRight");
                }

            }

            //haut - bas
            if (Mathf.Abs(GetComponentInParent<scrPlayerKnockBack>().currentKnockbackDirection.x) <= Mathf.Abs(GetComponentInParent<scrPlayerKnockBack>().currentKnockbackDirection.z))
            {

                if (GetComponentInParent<scrPlayerKnockBack>().currentKnockbackDirection.z < 0)
                {
                    playerAnimator.Play("iwkaHurtDown");
                }
                else
                {
                    playerAnimator.Play("iwkaHurtUp");

                }

            }

        }
        else
        {
            playerAnimator.SetBool("stun", false);
        }
        #endregion
    }

    void InCompetenceMenu()
    {
        if (GetComponentInParent<scrPlayerPaused>().playerPaused)
        {
            playerAnimator.speed = 0;
        }
    }

}