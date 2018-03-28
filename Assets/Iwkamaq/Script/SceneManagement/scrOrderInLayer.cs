using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrOrderInLayer : MonoBehaviour {

	public int offset;
	SpriteRenderer ObjectRenderer;

	// Use this for initialization
	void Start () {

		//offset = 0;

		ObjectRenderer = GetComponent<SpriteRenderer>();

	}

	// Update is called once per frame
	void Update () {

		ObjectRenderer.sortingOrder = -Mathf.FloorToInt(transform.position.z) + Mathf.FloorToInt(ObjectRenderer.size.y/2) + offset;

	}
}
