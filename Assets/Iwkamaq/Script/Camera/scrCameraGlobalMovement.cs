using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCameraGlobalMovement : MonoBehaviour {

    public static scrCameraGlobalMovement CameraManager;

    [Header("Speed")]
    public float maxCameraPlayerSpeed;
    public float maxCameraManualSpeed;
    public float maxCameraEventSpeed;
    float currentCameraSpeed;

    [Header("Position")]
    float posX;
    float posZ;
    Vector3 currentPosition;

    public Vector3 currentGoalPosition;

    private void Start()
    {
        CameraManager = this;
        currentCameraSpeed = maxCameraPlayerSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(GetComponent<scrCameraPlayerMovement>().isFollowingPlayer)
            {                
                GetComponent<scrCameraPlayerMovement>().isFollowingPlayer = false;
                GetComponent<scrCameraManualMovement>().posX = currentGoalPosition.x;
                GetComponent<scrCameraManualMovement>().posZ = currentGoalPosition.z;
                GetComponent<scrCameraManualMovement>().isManual = true;
            }
            else
            {
                if (GetComponent<scrCameraManualMovement>().isManual)
                {
                    GetComponent<scrCameraManualMovement>().isManual = false;
                    GetComponent<scrCameraEventMovement>().isEventing = true;
                }
                else
                {
                    if (GetComponent<scrCameraEventMovement>().isEventing)
                    {
                        GetComponent<scrCameraEventMovement>().isEventing = false;
                        GetComponent<scrCameraPlayerMovement>().isFollowingPlayer = true;
                    }
                }
            }
        }

        ChooseMovementMode();

        
    }

    private void FixedUpdate()
    {
        if (!scrCompetenceMenu.CompetenceMenu.inCompetenceMenu)
            MoveTowardGoal();
    }

    void MoveTowardGoal()
    {
        if (Vector3.Distance(transform.position, currentGoalPosition) > 0.05f)
        {
            posX = Mathf.Lerp(transform.position.x, currentGoalPosition.x, Time.deltaTime * currentCameraSpeed);
            posZ = Mathf.Lerp(transform.position.z, currentGoalPosition.z, Time.deltaTime * currentCameraSpeed);

            currentPosition = new Vector3(posX, 10, posZ);

            transform.position = currentPosition;
        }                    
    }

    void ChooseMovementMode()
    {
        if (GetComponent<scrCameraPlayerMovement>().isFollowingPlayer)
        {
            currentGoalPosition = GetComponent<scrCameraPlayerMovement>().goalPosition;
            currentCameraSpeed = maxCameraPlayerSpeed;
        }
        else
        {
            if (GetComponent<scrCameraManualMovement>().isManual)
            {
                currentGoalPosition = GetComponent<scrCameraManualMovement>().goalPosition;
                currentCameraSpeed = maxCameraManualSpeed;
            }
            else
            {
                if (GetComponent<scrCameraEventMovement>().isEventing)
                {
                    currentGoalPosition = GetComponent<scrCameraEventMovement>().currentEventPosition;
                    currentCameraSpeed = maxCameraEventSpeed;
                }
            }
        }
    }

    public void SetCameraShake(string mode, float amount, float duration)
    {
        if (mode != "both" && mode != "vertical" && mode != "horizontal")
            mode = "both";

        if (mode == "both")
        {
            GetComponent<scrCameraShake>().both = true;
            GetComponent<scrCameraShake>().vertical = false;
            GetComponent<scrCameraShake>().horizontal = false;
        }
        else
        {
            if (mode == "vertical")
            {
                GetComponent<scrCameraShake>().both = false;
                GetComponent<scrCameraShake>().vertical = true;
                GetComponent<scrCameraShake>().horizontal = false;
            }
            else
            {
                if (mode == "horizontal")
                {
                    GetComponent<scrCameraShake>().both = false;
                    GetComponent<scrCameraShake>().vertical = false;
                    GetComponent<scrCameraShake>().horizontal = true;
                }
            }
        }

        GetComponent<scrCameraShake>().shakeAmount = amount;
        GetComponent<scrCameraShake>().shakeDuration = duration;
    }
}
