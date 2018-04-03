using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCameraShake : MonoBehaviour {

    [Header("Modes")]
    public bool vertical;
    public bool horizontal;
    public bool both;

    [Header("Shaking Values")]
    public float shakeAmount;
    public float shakeDuration;

	void Start () {
		
	}
	
	void Update () {
        if (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime;
            if (both)
                BothShake();
            else
            {
                if (vertical)
                    VerticalShake();
                else
                {
                    if (horizontal)
                        HorizontalShake();
                }
            }
        }
        if (shakeDuration < 0)
            shakeDuration = 0;
	}

    void BothShake()
    {
        transform.position += new Vector3(Random.Range(-shakeAmount, shakeAmount), 0, Random.Range(-shakeAmount, shakeAmount));
    }

    void VerticalShake()
    {
        transform.position += new Vector3(0, 0, Random.Range(-shakeAmount, shakeAmount));
    }

    void HorizontalShake()
    {
        transform.position += new Vector3(Random.Range(-shakeAmount, shakeAmount), 0, 0);
    }
}
