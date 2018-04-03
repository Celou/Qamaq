using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCameraPlayerMovement : MonoBehaviour {

    [Header("Player")]
    public Transform player;
    public float maxDistanceFromPlayer;

    [Header("Speed")]
    public float cameraSpeed;

    [Header("Positions")]
    public bool isFollowingPlayer;
    float posX;
    float posZ;
    public Vector3 goalPosition;

    void Update () {
        CameraPositionUpdate();

        if (Input.GetKeyDown(KeyCode.Space))
            RecenterCameraOnPlayer();
    }

    void CameraPositionUpdate()
    {
        posX += player.GetComponent<Rigidbody>().velocity.normalized.x * cameraSpeed;
        posZ += player.GetComponent<Rigidbody>().velocity.normalized.z * cameraSpeed;

        posX = Mathf.Clamp(posX, -maxDistanceFromPlayer, maxDistanceFromPlayer);
        posZ = Mathf.Clamp(posZ, -maxDistanceFromPlayer, maxDistanceFromPlayer);

        goalPosition = player.position + new Vector3(posX, 10, posZ);
    }

    void RecenterCameraOnPlayer()
    {
        posX = 0;
        posZ = 0;
    }
}
