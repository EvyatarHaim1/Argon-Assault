using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float controlSpeed = 20f;
    [Tooltip("In ms^-1")] [SerializeField] float xRange = 15f;
    [Tooltip("In ms^-1")] [SerializeField] float yRange = 8f;

    [Header("Screen-Position Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = -5f;

    [Header("Screen-throw Based")]
    [SerializeField] float controlPitchFactor = -29f;
    [SerializeField] float controlRollFactor = -29f;


    float xThrow, yThrow;
    bool isControlEnabled = true;
    // Start is called before the first frame update
    

   
    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
        ProcessTranslation();
        ProcessRotation();
        }
    }

    void OnPlayerDeath()
    {
        isControlEnabled = false;
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;
            ;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
