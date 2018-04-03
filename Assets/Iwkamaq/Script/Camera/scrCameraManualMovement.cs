using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCameraManualMovement : MonoBehaviour {

    [Header("Speed")]
    public float cameraSpeed;

    [Header("Positions")]
    public bool isManual;
    public float posX;
    public float posZ;
    public Vector3 goalPosition;

    private void Update()
    {
        CameraPositionUpdate();
    }

    void CameraPositionUpdate()
    {
        //#region base movements
        //version 3.3.4 : comme si c'était un joueur + vitesse constante
        if (Input.GetKey(KeyCode.Z))
        {
            posZ += cameraSpeed * Time.timeScale;
        }

        if (Input.GetKey(KeyCode.S))
        {
            posZ -= cameraSpeed * Time.timeScale;
        }

        if (Input.GetKey(KeyCode.D))
        {
            posX += cameraSpeed * Time.timeScale;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            posX -= cameraSpeed * Time.timeScale;
        }

        goalPosition = new Vector3(posX, 10, posZ);
    }
}
