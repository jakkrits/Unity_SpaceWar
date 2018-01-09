using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 4f;
    [Tooltip("In ms")] [SerializeField] float xRange = 3.5f;

    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 4f;
    [Tooltip("In ms")] [SerializeField] float yRange = 2.0f;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        UpdateXPosition();
        UpdateYPosition();
    }

    private void UpdateXPosition() {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffsetThisFrame = xThrow * xSpeed * Time.deltaTime;

        float rawNextXPosition = xOffsetThisFrame + transform.localPosition.x;
        float limitXPositionInScreen = Mathf.Clamp(rawNextXPosition, -xRange, xRange);

        transform.localPosition = new Vector3(
            limitXPositionInScreen,
            transform.localPosition.y,
            transform.localPosition.z
        );
    }

    private void UpdateYPosition() {
        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffsetThisFrame = yThrow * ySpeed * Time.deltaTime;

        float rawNextYPosition = yOffsetThisFrame + transform.localPosition.y;
        float limitYPositionInScreen = Mathf.Clamp(rawNextYPosition, -yRange, 0);

        transform.localPosition = new Vector3(
            transform.localPosition.x,
            limitYPositionInScreen,
            transform.localPosition.z
        );
    }

}
