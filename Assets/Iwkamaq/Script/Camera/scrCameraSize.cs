using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCameraSize : MonoBehaviour {

    [Header("Sizes")]
    float basicSize;
    float currentSize;
    float currentGoalSize;

    [Header("Speeds")]
    public float sizeChangeSpeed;


	void Start () {
        basicSize = GetComponent<Camera>().orthographicSize;

        currentGoalSize = basicSize;
        currentSize = basicSize;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Keypad8))
            currentGoalSize += 1;

        if (Input.GetKeyDown(KeyCode.Keypad2))
            currentGoalSize -= 1;

        if (Input.GetKeyDown(KeyCode.Space))
            currentGoalSize = basicSize;

        CurrentSizeUpdate();
    }

    void CurrentSizeUpdate()
    {
        if (Mathf.Abs(currentSize - currentGoalSize) > 0.05f)
        {
            currentSize = Mathf.Lerp(currentSize, currentGoalSize, Time.deltaTime * sizeChangeSpeed);

            GetComponent<Camera>().orthographicSize = currentSize;
        }
        else
            GetComponent<Camera>().orthographicSize = currentGoalSize;
    }
}
