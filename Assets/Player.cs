using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 20f;
    [Tooltip("In ms")] [SerializeField] float xRange = 5f;

    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 10f;
    [Tooltip("In ms")] [SerializeField] float yRange = 3f;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -1.5f;

    [SerializeField] float positionYawFactor = -2f;

    [SerializeField] float controlRollFactor = 50f;

    float xThrow, yThrow;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        UpdateXPosition();
        UpdateYPosition();
        UpdateRotation();
    }

    private void UpdateXPosition() {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
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
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffsetThisFrame = yThrow * ySpeed * Time.deltaTime;

        float rawNextYPosition = yOffsetThisFrame + transform.localPosition.y;
        float limitYPositionInScreen = Mathf.Clamp(rawNextYPosition, -yRange, yRange);

        transform.localPosition = new Vector3(
            transform.localPosition.x,
            limitYPositionInScreen,
            transform.localPosition.z
        );
    }

    private void UpdateRotation() {
        float picthDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = picthDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
