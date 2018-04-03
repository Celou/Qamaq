using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrGetCapsuleColliderRectangleDimension : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        Vector3 BoxDimensions = new Vector3(Mathf.Clamp(GetComponent<CapsuleCollider>().height - GetComponent<CapsuleCollider>().radius * 2, 0, Mathf.Infinity), 0.2f, GetComponent<CapsuleCollider>().radius * 2);

        if (GetComponent<CapsuleCollider>().direction == 0)
            GetComponent<BoxCollider>().size = new Vector3(BoxDimensions.x, BoxDimensions.z, BoxDimensions.y);
        else
        {
            if (GetComponent<CapsuleCollider>().direction == 1)
                GetComponent<BoxCollider>().size = new Vector3(BoxDimensions.z, BoxDimensions.x, BoxDimensions.y);
        }
	}       
}
