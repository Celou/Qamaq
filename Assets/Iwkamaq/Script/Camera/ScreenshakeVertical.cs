using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshakeVertical : MonoBehaviour {

	//Le Transform de la camera
	public Transform camTransform;
	// Combien de temps le screenshake va durer
	public float shakeDuration = 0f;
	// Plus c'est fort, plus ça secoue, le decreasefactor c'est comment ça s'arrête normalement
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	bool shaking;
	public Vector3 originalPos;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		shaking = false;
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		if (shakeDuration > 0)
		{
			if (!shaking)
			{
				shaking = true;
				originalPos = camTransform.localPosition;
			}

			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			if(tag == "MainCamera")
				camTransform.localPosition = new Vector3(originalPos.x, originalPos.y, camTransform.localPosition.z);
			else
				camTransform.localPosition = new Vector3(originalPos.x, camTransform.localPosition.y, originalPos.z);

			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			if (shaking)
			{
				shaking = false;
				camTransform.localPosition = originalPos;
			}

			shakeDuration = 0f;
			//camTransform.localPosition = originalPos;
		}


	}
}
