using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float controlSpeed = 15f;
    [SerializeField] private float xBorder = 10f;
    [SerializeField] private float yBorder = 5f;
    [SerializeField] private float positionPitchFactor = -2f;
    [SerializeField] private float controlPitchFactor = -10f;
    [SerializeField] private float positionYawFactor = 2f;
    [SerializeField] private float controlRollFactor = -15f;
    [SerializeField] private InputAction movement;

    private float xThrow;
    private float yThrow;

    private void OnEnable()
    {
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xBorder, xBorder);

        float yOffset = yThrow * controlSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yBorder, yBorder + 3);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
