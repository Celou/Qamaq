using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrRedMaskDamages : MonoBehaviour {

    [Header("Visual Variables")]
    SpriteRenderer maskRenderer;

    [Header("Time Variables")]
    public float baseRedDuration;
    public float currentRedDuration;
	
    void Start ()
    {
        baseRedDuration = 1;
        maskRenderer = GetComponent<SpriteRenderer>();
    }
    	
	void Update () {

        if(currentRedDuration > 0)
        {
            currentRedDuration -= Time.deltaTime;            
        }
        if (currentRedDuration < 0)
            currentRedDuration = 0;

        maskRenderer.color = new Color(1, 1, 1, 0.75f * Mathf.Sqrt(currentRedDuration / baseRedDuration));
        
    }
}
