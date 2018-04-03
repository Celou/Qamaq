using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerKnockBack : MonoBehaviour {

    [Header("Knockback")]
    public float currentKnockbackForce;
    public float takenKnockbackForce;
    public float knockbackSlowingDownVariable;
    public Vector3 currentKnockbackDirection;
    public bool knockbacked;

    [Header("Other Components")]
    Rigidbody playerBody;

    // Use this for initialization
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(knockbacked && !GetComponent<scrPlayerPaused>().playerPaused)
            KnockBackUpdate();
    }

    private void FixedUpdate()
    {
        if(!GetComponent<scrPlayerPaused>().playerPaused)
            KnockbackMovement();
    }

    void KnockBackUpdate()
    {
        // fin du knockback quand la force tombe à z&ro
        if (currentKnockbackForce < 0.1f)
        {
            currentKnockbackForce = 0;
            playerBody.velocity = Vector3.zero;
            knockbacked = false;
        }

        if (currentKnockbackForce > 0)
        {
            if (currentKnockbackForce < 1)
                currentKnockbackForce = 0;
        }

    }

    void KnockbackMovement()
    {
        if (currentKnockbackForce > 0)
        {
            //on déplace le joueur selon le knockback
            playerBody.velocity = 4 * currentKnockbackDirection * currentKnockbackForce * Time.deltaTime;

            //on fait diminuer la force du Knockback
            currentKnockbackForce = Mathf.Lerp(currentKnockbackForce, 0, Time.deltaTime * (knockbackSlowingDownVariable * (1.5f - currentKnockbackForce / takenKnockbackForce)));
        }
    }

    public void GetPlayerKnockback(float knockbackForce, Vector3 knockbackDirection)
    {
        currentKnockbackForce = knockbackForce;
        takenKnockbackForce = knockbackForce;
        currentKnockbackDirection = knockbackDirection.normalized;
        knockbacked = true;
    }
}
