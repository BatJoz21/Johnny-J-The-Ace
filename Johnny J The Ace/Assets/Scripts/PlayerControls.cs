using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Aircraft's Movement")]
    [Tooltip("How fast aircraft moves up and down")] [SerializeField] private float controlSpeed = 15f;
    [Tooltip("Border in X Axis so the aircraft doesn't move outside of the camera")] [SerializeField] private float xBorder = 10f;
    [Tooltip("Border in Y Axis so the aircraft doesn't move outside of the camera")] [SerializeField] private float yBorder = 5f;
    [SerializeField] private float positionPitchFactor = -2f;
    [SerializeField] private float controlPitchFactor = -10f;
    [SerializeField] private float positionYawFactor = 2f;
    [SerializeField] private float controlRollFactor = -15f;

    [Header("Input Action")]
    [SerializeField] private InputAction movement;
    [SerializeField] private InputAction shooting;

    [Header("Aircraft's Guns")]
    [SerializeField] private ParticleSystem[] guns;

    private float xThrow;
    private float yThrow;

    private void OnEnable()
    {
        movement.Enable();
        shooting.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        shooting.Disable();
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
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

    private void ProcessFiring()
    {
        if (shooting.ReadValue<float>() > 0)
        {
            SetActivateGuns(true);
        }
        else
        {
            SetActivateGuns(false);
        }
    }

    private void SetActivateGuns(bool val)
    {
        foreach (ParticleSystem gun in guns)
        {
            var gunEmission = gun.emission;
            gunEmission.enabled = val;
        }
    }
}
