using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCamera : MonoBehaviour
{
    public static scrCamera CameraManager;

    Camera selfCamera;
    public bool followingThePlayer;
    public Transform Player;
    Vector3 Position;



    float launchTime;

    [Header("Camera Diplacement")]
    public float interpolationCoefficient;
    public float cameraSlideCoeff;
    public float cameraSpeed;
    public float posX;
    public float posZ;
    Vector3 dir;
    public float maxDistanceFromPlayer;
    public float transitionTime;
    float currentTransitionTime;
    public float immobilizationTime;
    float currentImmobilizationTime;

    [Header("CameraSize")]
    public float cameraSizeChangeSpeed;
    float baseCameraSize;
    float goalCameraSize;
    float currentCameraSize;

    [Header("Camera Recenter")]
    public float cameraRecenterSpeed;
    public bool cameraRecentering;
    Vector3 recenteringVector;

    [Header("Filter")]
    public GameObject filter;
    public SpriteRenderer filterRenderer;
    //Transform baseFilterTransform;
    float baseXScale;
    float baseYScale;

    [Header("Canvas")]
    public Canvas gameCanvas;

    [Header("Event")]
    public bool eventing;
    public float currentEventCameraSpeed;
    public Vector3 StartPosition;
    public Vector3 GoalPosition;

    [Header("ManualGesture")]
    public float manualCameraSpeed;
    float manualDirX;
    float manualDirZ;
    bool manual;

    // Use this for initialization
    void Start()
    {
        CameraManager = this;
        selfCamera = GetComponent<Camera>();
        baseCameraSize = selfCamera.orthographicSize;
        Position = Player.GetComponent<scrPlayer>().transform.position;
        launchTime = 0;
        cameraRecentering = false;
        baseXScale = filter.transform.localScale.x;
        baseYScale = filter.transform.localScale.y;
        //Debug.Log(baseFilterTransform.localScale + "mdr");
    }

    // Update is called once per frame
    void Update()
    {
        if (followingThePlayer
            && /*!scrExperienceManager.ExperienceManager.inCompetenceMenu &&*/ launchTime <= 0
            /*&& GetComponent<Screenshake>().shakeDuration <= 0*/
            && GetComponent<ScreenshakeVertical>().shakeDuration <= 0
            && GetComponent<ScreenshakeHorizontal>().shakeDuration <= 0
            && !eventing
            && !manual)
        {

            CameraPositionUpdate();

        }

        if (eventing)
        {
            EventMovementUpdate();
        }

        if (!eventing)
            RecenterCamera();

        ManualMovementUpdate();

        FilterUpdate();

        GetComponent<Screenshake>().originalPos = transform.position;
        GetComponent<ScreenshakeVertical>().originalPos = transform.position;
        GetComponent<ScreenshakeHorizontal>().originalPos = transform.position;

        //CanvasUpdate();

        CameraSizeUpdate();

    }

    void CameraPositionUpdate()
    {
        //#region base movements
        //version 3.3.4 : comme si c'était un joueur + vitesse constante
        if (Input.GetKey(KeyCode.Z) && posZ < maxDistanceFromPlayer && currentImmobilizationTime == 0)
        {

            posZ += cameraSpeed * Time.timeScale;

        }

        if (Input.GetKey(KeyCode.S) && posZ > -maxDistanceFromPlayer && currentImmobilizationTime == 0)
        {
            posZ -= cameraSpeed * Time.timeScale;

        }

        if (Input.GetKey(KeyCode.D) && posX < maxDistanceFromPlayer && currentImmobilizationTime == 0)
        {
            posX += cameraSpeed * Time.timeScale;

        }

        if (Input.GetKey(KeyCode.Q) && posX > -maxDistanceFromPlayer && currentImmobilizationTime == 0)
        {
            posX -= cameraSpeed * Time.timeScale;

        }


        if (Input.GetKeyDown(KeyCode.Z) && (posZ < 0 || Mathf.Abs(posX) > maxDistanceFromPlayer / 10))
        {

            currentImmobilizationTime = immobilizationTime;

        }

        if (Input.GetKeyDown(KeyCode.S) && (posZ > 0 || Mathf.Abs(posX) > maxDistanceFromPlayer / 10))
        {

            currentImmobilizationTime = immobilizationTime;

        }

        if (Input.GetKeyDown(KeyCode.D) && (posX < 0 || Mathf.Abs(posZ) > maxDistanceFromPlayer / 10))
        {

            currentImmobilizationTime = immobilizationTime;

        }

        if (Input.GetKeyDown(KeyCode.Q) && (posX > 0 || Mathf.Abs(posZ) > maxDistanceFromPlayer / 10))
        {

            currentImmobilizationTime = immobilizationTime;

        }


        if (Input.GetKeyUp(KeyCode.Z) && (posZ > 0 || Mathf.Abs(posX) > maxDistanceFromPlayer / 10))
        {

            currentImmobilizationTime = immobilizationTime;

        }

        if (Input.GetKeyUp(KeyCode.S) && (posZ < 0 || Mathf.Abs(posX) > maxDistanceFromPlayer / 10))
        {

            currentImmobilizationTime = immobilizationTime;

        }

        if (Input.GetKeyUp(KeyCode.D) && (posX > 0 || Mathf.Abs(posZ) > maxDistanceFromPlayer / 10))
        {

            currentImmobilizationTime = immobilizationTime;

        }

        if (Input.GetKeyUp(KeyCode.Q) && (posX < 0 || Mathf.Abs(posZ) > maxDistanceFromPlayer / 10))
        {

            currentImmobilizationTime = immobilizationTime;

        }

        if (currentTransitionTime > 0)
            currentTransitionTime -= Time.deltaTime;

        if (currentTransitionTime < 0)
            currentTransitionTime = 0;

        if (currentImmobilizationTime > 0)
            currentImmobilizationTime -= Time.deltaTime;

        if (currentImmobilizationTime < 0)
        {
            currentImmobilizationTime = 0;
            currentTransitionTime = transitionTime;
        }


        if (Mathf.Abs(posZ) > 1)
            posZ = Mathf.Sign(posZ);

        if (Mathf.Abs(posX) > 1)
            posX = Mathf.Sign(posX);


        if (Mathf.Abs(posZ) < 0.5f * cameraSpeed || (cameraRecentering && Mathf.Abs(posZ) < 0.5f * cameraRecenterSpeed))
            posZ = 0;

        if (Mathf.Abs(posX) < 0.5f * cameraSpeed || (cameraRecentering && Mathf.Abs(posZ) < 0.5f * cameraRecenterSpeed))
            posX = 0;

        dir = new Vector3(posX, 10, posZ);

        transform.position = Player.GetComponent<scrPlayer>().transform.position + dir;

    }

    void RecenterCamera()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !cameraRecentering)
        {
            Debug.Log("rencentrage");
            cameraRecentering = true;
            recenteringVector = Player.GetComponent<scrPlayer>().transform.position - transform.position;

        }

        if (cameraRecentering == true)
        {
            posX += recenteringVector.x / 5;
            posZ += recenteringVector.z / 5;
        }

        if (transform.position.x == Player.GetComponent<scrPlayer>().transform.position.x && transform.position.z == Player.GetComponent<scrPlayer>().transform.position.z)
        {

            cameraRecentering = false;
        }


    }

    void CanvasUpdate()
    {

        //gameCanvas.sortingOrder = Player.GetComponent<scrPlayer>().PlayerRenderer.sortingOrder + 500;

    }

    void FilterUpdate()
    {

        filter.transform.localScale = new Vector3(baseXScale, baseYScale, 1) * (selfCamera.orthographicSize / baseCameraSize);

    }

    void EventMovementUpdate()
    {
        if (Vector3.Distance(transform.position, GoalPosition) > 0.2f)
        {
            posX = Mathf.Lerp(transform.position.x, GoalPosition.x, Time.deltaTime * currentEventCameraSpeed);
            posZ = Mathf.Lerp(transform.position.z, GoalPosition.z, Time.deltaTime * currentEventCameraSpeed);

            dir = new Vector3(posX, 10, posZ);

            transform.position = dir;
        }
    }

    void CameraSizeUpdate()
    {
        if (Mathf.Abs(currentCameraSize - goalCameraSize) > 0.1f)
        {
            Debug.Log("ça change");
            currentCameraSize = Mathf.Lerp(currentCameraSize, goalCameraSize, Time.deltaTime * cameraSizeChangeSpeed);
        }
        else
            currentCameraSize = goalCameraSize;

        selfCamera.orthographicSize = currentCameraSize;
        transform.localScale = new Vector3(currentCameraSize / baseCameraSize, currentCameraSize / baseCameraSize, currentCameraSize / baseCameraSize);

    }

    void ManualMovementUpdate()
    {
        float currentManualSwitchTime = 0;

        if (Input.GetKeyDown(KeyCode.RightShift) && !manual && currentManualSwitchTime == 0)
        {
            manual = true;
            currentManualSwitchTime = 1;
        }

        if (Input.GetKeyDown(KeyCode.RightShift) && manual && currentManualSwitchTime == 0)
        {
            manual = false;
            currentManualSwitchTime = 1;
        }

        if (manual)
        {
            manualDirX = Input.GetAxis("Horizontal");
            manualDirZ = Input.GetAxis("Vertical");
            transform.Translate(new Vector3(manualDirX, 0, manualDirZ).normalized * Time.deltaTime * manualCameraSpeed, Space.World);

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                Debug.Log("dezoom");
                goalCameraSize += 1;
            }
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                Debug.Log("zoom");
                goalCameraSize -= 1;
            }
            //Debug.Log("goal : " + goalCameraSize);
            Debug.Log("current : " + currentCameraSize);
        }
        else
        {
            currentCameraSize = baseCameraSize;
            goalCameraSize = currentCameraSize;
        }
    }
}