using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrDashTrace : MonoBehaviour {

    SpriteRenderer traceRenderer;
    public float totalLifeTime;
    public float currentLifeTime;

    void Start()
    {
        traceRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        currentLifeTime -= Time.deltaTime;

        traceRenderer.color = new Color(0, 1, 1, currentLifeTime);

        if (currentLifeTime <= 0)
            Destroy(gameObject);
	}
}
