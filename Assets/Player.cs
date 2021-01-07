using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")][SerializeField] float speed = 20f;
    [Tooltip("In ms^-1")] [SerializeField] float xRange = 15f;
    [Tooltip("In ms^-1")] [SerializeField] float yRange = 8f;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = -5f;
    [SerializeField] float controlPitchFactor = -29f;
    [SerializeField] float controlRollFactor = -29f;


    float xThrow, yThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        print("Player triggered something");
    }
    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;

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
