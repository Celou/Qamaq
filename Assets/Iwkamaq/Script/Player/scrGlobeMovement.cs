using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrGlobeMovement : MonoBehaviour {

    [Header("Globe Visuals")]
    public SpriteRenderer globeReflect;
    public Animator globeAnimator;

    [Header("Globe Speed")]
    public float maxGlobeDistance;
    public float globeMaxSpeed;
    public float globeMinSpeed;
    public float globeFollowingPlayerSpeed;
    public float minDistance;
    float globeCurrentSpeed;
    public float globeAcceleration;
    public Vector3 globeAimingPosition;

    [Header("Other Objects")]
    Transform pivot;

    [Header("Reflect")]
    public GameObject reflectObject;

    private void Start()
    {
        pivot = GetComponentInParent<scrPlayerLookingDirection>().pivot;
    }

    private void Update()
    {
        if (!GetComponentInParent<scrPlayerPaused>().playerPaused)
        {
            globeAnimator.speed = 1;
            GlobeUpdate();
        }
        else
            globeAnimator.speed = 0;

        ReflectUpdate();
    }

    void GlobeUpdate()
    {
        #region deuxieme test globe
        //suit le curseur avec un distance maximum du personnage

        //on détermine la position que doit occuper le globe
        if (Vector3.Distance(pivot.position, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, pivot.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).z)) < maxGlobeDistance)
        {
            globeAimingPosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, pivot.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
        }
        else
        {
            globeAimingPosition = pivot.position + pivot.up * maxGlobeDistance;
        }

        //on déplace le globe vers la position cible, s'il n'y est pas encore
        if (Vector3.Distance(transform.position, globeAimingPosition) > globeCurrentSpeed * Time.deltaTime)
            transform.Translate((globeAimingPosition - transform.position).normalized * globeCurrentSpeed * Time.deltaTime, Space.World);

        //gestion de la vitesse du globe
        if (Vector3.Distance(pivot.position, transform.position) <= maxGlobeDistance)
        {
            if (Vector3.Distance(transform.position, globeAimingPosition) > minDistance && globeCurrentSpeed < globeMaxSpeed)
                globeCurrentSpeed += globeAcceleration;
            if (globeCurrentSpeed > globeMaxSpeed)
                globeCurrentSpeed = globeMaxSpeed;

            if (Vector3.Distance(transform.position, globeAimingPosition) < minDistance && globeCurrentSpeed > globeMinSpeed)
                globeCurrentSpeed -= globeAcceleration;
            if (globeCurrentSpeed < globeMinSpeed)
                globeCurrentSpeed = globeMinSpeed;
        }

        else
        {
            if (globeCurrentSpeed < globeFollowingPlayerSpeed)
                globeCurrentSpeed += globeAcceleration;
            if (globeCurrentSpeed > globeFollowingPlayerSpeed)
                globeCurrentSpeed = globeFollowingPlayerSpeed;
        }
        #endregion

        #region reflet globe
        globeReflect.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        #endregion

        #region sons
        /*if (!globeMouvementSound1.isPlaying && !globeMouvementSound2.isPlaying && !globeMouvementSound3.isPlaying && globeCurrentSpeed == globeMaxSpeed)
        {
            int randomGlobeMouvementSoundVariable = Random.Range(0, 3);

            if (randomGlobeMouvementSoundVariable == 0)
                globeMouvementSound1.Play();

            if (randomGlobeMouvementSoundVariable == 1)
                globeMouvementSound2.Play();

            if (randomGlobeMouvementSoundVariable == 2)
                globeMouvementSound3.Play();
        }
        if (globeCurrentSpeed < globeMaxSpeed / 2 && (globeMouvementSound1.isPlaying || globeMouvementSound2.isPlaying || globeMouvementSound3.isPlaying))
        {
            globeMouvementSound1.Play();
            globeMouvementSound2.Play();
            globeMouvementSound3.Play();
        }*/
        #endregion
    }

    void ReflectUpdate()
    {
        globeReflect.sprite = GetComponent<SpriteRenderer>().sprite;
        globeReflect.transform.position = transform.position + new Vector3(0, 0, -3);
    }
}
