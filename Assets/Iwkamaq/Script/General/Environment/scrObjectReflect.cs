using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrObjectReflect : MonoBehaviour {

    public Vector3 reflectPosition;
    SpriteRenderer ObjectRenderer;
    GameObject objectReflect;

    void Start () {

        reflectPosition = transform.position;

        if (GetComponent<SpriteRenderer>() != null)
        {
            ObjectRenderer = GetComponent<SpriteRenderer>();
            reflectPosition.z = transform.position.z - ObjectRenderer.size.y;
            objectReflect = Instantiate(new GameObject(), reflectPosition, transform.rotation, transform);
            objectReflect.transform.localScale = new Vector3(1, -1, 1);
            objectReflect.AddComponent<SpriteRenderer>();
            objectReflect.GetComponent<SpriteRenderer>().sortingOrder = -10000;
        }

    }
	
	void Update () {
        objectReflect.GetComponent<SpriteRenderer>().sprite = ObjectRenderer.sprite;
    }
}
