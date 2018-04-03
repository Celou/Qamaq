using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerMovementUpdate : MonoBehaviour {

    [Header("Physics")]
    Rigidbody playerBody;

    [Header("Direction")]
    float walkX;
    float walkZ;
    public Vector3 dir;

    [Header("Speed")]
    public float walkingSpeed;
    public float sprintSpeed;
    public float currentSpeed;
    public float accelerationCoeff;

    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MovementUpdate();
    }

    private void FixedUpdate()
    {
        if(!GetComponent<scrPlayerPunchUpdate>().punching && !GetComponent<scrDash>().dashing && !GetComponent<scrPlayerKnockBack>().knockbacked && !GetComponent<scrPlayerPaused>().playerPaused)
            playerBody.velocity = dir * currentSpeed * Time.deltaTime;
    }

    void MovementUpdate()
    {

        //vitesse du joueur         
        if (scrCompetenceManager.CompetenceManager.protectionSphereOn == true || scrCompetenceManager.CompetenceManager.vendettaSphereOn == true)
            return;

        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = (sprintSpeed) * scrCompetenceManager.CompetenceManager.currentSpeedBoostAmount;
        else
            currentSpeed = (walkingSpeed) * scrCompetenceManager.CompetenceManager.currentSpeedBoostAmount;

        //tests pour un nouveau système de déplacements
        if (!GetComponent<scrPlayerPunchUpdate>().punching && !GetComponent<scrDash>().dashing)
        {

            if (Input.GetKey(KeyCode.Z) && walkZ < 1)
                walkZ += accelerationCoeff;

            if (Input.GetKey(KeyCode.S) && walkZ > -1)
                walkZ -= accelerationCoeff;

            if (Input.GetKey(KeyCode.D) && walkX < 1)
                walkX += accelerationCoeff;

            if (Input.GetKey(KeyCode.Q) && walkX > -1)
                walkX -= accelerationCoeff;
        }

        if (!Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.S) && walkZ != 0)
        {
            walkZ -= Mathf.Sign(walkZ) * accelerationCoeff;
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.Q) && walkX != 0)
            walkX -= Mathf.Sign(walkX) * accelerationCoeff;

        if (Mathf.Abs(walkZ) > 1)
            walkZ = Mathf.Sign(walkZ);

        if (Mathf.Abs(walkX) > 1)
            walkX = Mathf.Sign(walkX);

        if (Mathf.Abs(walkZ) < 0.5f * accelerationCoeff)
            walkZ = 0;

        if (Mathf.Abs(walkX) < 0.5f * accelerationCoeff)
            walkX = 0;

        dir = new Vector3(walkX, 0, walkZ);
        if (Mathf.Abs(walkX) > 0.75f && Mathf.Abs(walkZ) > 0.75f)
            dir = dir.normalized;

    }

    /*private void OnCollisionStay(Collision collision)
    {
        //tentative pour éviter que le joueur soit bloqué dans un mur 
        if(playerBody.velocity == Vector3.zero && Input.GetKey(KeyCode.Return))
            transform.Translate(new Vector3(transform.position.x - collision.transform.position.x, 0, transform.position.z - collision.transform.position.z).normalized, Space.World);        
    }*/
}
