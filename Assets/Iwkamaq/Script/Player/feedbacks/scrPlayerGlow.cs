using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerGlow : MonoBehaviour {

    public SpriteRenderer playerRenderer;
    SpriteRenderer playerGlow;

    void Start()
    {
        playerGlow = GetComponent<SpriteRenderer>();
    }

    void Update () {
        playerGlow.sprite = playerRenderer.sprite;
	}
}
