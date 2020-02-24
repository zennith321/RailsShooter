using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject[] guns;
    [Header("Player Speed")]
    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 20f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 20f;
    [Header("Screen Restrictions")]
    [SerializeField] float minXRange = -20f;
    [SerializeField] float maxXRange = 20f;
    [SerializeField] float minYRange = -14f;
    [SerializeField] float maxYRange = 14f;
    [Header("Control Throw")]
    [SerializeField] float positionPitchFactor = -1f;
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float positionYawFactor = 1f;
    [SerializeField] float controlYawFactor = 15f;
    [SerializeField] float controlRollFactor = -25f;

    float xThrow;
    float yThrow;
    bool isControlEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void OnPlayerDeath()    // called by string reference
    {
        isControlEnabled = false;
    }

    private void ProcessTranslation() // TODO refactor to make neater
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;

        float rawNewXPos = transform.localPosition.x + xOffset;
        float newXPos = Mathf.Clamp(rawNewXPos, minXRange, maxXRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        float rawNewYPos = transform.localPosition.y + yOffset;
        float newYPos = Mathf.Clamp(rawNewYPos, minYRange, maxYRange);

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }
    
    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch =  pitchDueToControlThrow + pitchDueToPosition;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float yawDueToControlThrow = xThrow * controlYawFactor;
        float yaw = yawDueToControlThrow + yawDueToPosition;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    
    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns) // care may affect death FX
        {
            ParticleSystem ps = gun.GetComponent<ParticleSystem>();
            var em = ps.emission;
            em.enabled = isActive;
        }
    }
}
