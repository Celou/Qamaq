using UnityEngine;
using System.Collections;
//Globalement ce script on va l'attacher sur la caméra, et dans les autres scripts on fera varier les valeurs ici présentes 
//pour avoir de bons screenshakes. Je te ferai même un doc Seb pour des presets de screenshake une fois que j'aurai
//suffisament testé, en t'indiquant où mettre les différentes valeurs du shake.
//Ca marche assi sur des props, donc on pourra shake des objets, la map même, ou bien la barre de compétences. Nice non ?
public class Screenshake : MonoBehaviour
{
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

			if (tag == "MainCamera")
				camTransform.localPosition = new Vector3(camTransform.localPosition.x, originalPos.y, camTransform.localPosition.z);
			else
				camTransform.localPosition = new Vector3(camTransform.localPosition.x, camTransform.localPosition.y, originalPos.z);

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