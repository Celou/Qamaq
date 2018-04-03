using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrObjectKnockback : MonoBehaviour {

    [Header("Knockback")]
    public float currentKnockbackForce;
    public float takenKnockbackForce;
    public float knockbackSlowingDownVariable;
    public Vector3 currentKnockbackDirection;

    [Header("Other Components")]
    Rigidbody body;

    // Use this for initialization
    void Start () {
        body = GetComponentInParent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        KnockBackUpdate();
    }

    private void FixedUpdate()
    {
        KnockbackMovement();
    }

    void KnockBackUpdate()
    {
        // fin du knockback quand la force tombe à z&ro
        if (currentKnockbackForce < 0)
        {
            currentKnockbackForce = 0;
            //knockbackSlowingDownVariable = 0;
            body.velocity = Vector3.zero;
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
            body.velocity = 4 * currentKnockbackDirection * currentKnockbackForce * Time.deltaTime;

            //on fait diminuer la force du Knockback
            currentKnockbackForce = Mathf.Lerp(currentKnockbackForce, 0, Time.deltaTime * (knockbackSlowingDownVariable * (1.5f - currentKnockbackForce / takenKnockbackForce)));
        }
     }

    public void GetKnockback(float knockbackForce, Vector3 knockbackDirection)
    {
        currentKnockbackForce = knockbackForce;
        takenKnockbackForce = knockbackForce;
        currentKnockbackDirection = knockbackDirection;
    }
}
