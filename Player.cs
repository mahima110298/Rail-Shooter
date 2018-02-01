using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    float xThrow;
    float yThrow;
    [Tooltip("in ms^-1")] [SerializeField] float speed = 20f;
    [Tooltip("in m")] [SerializeField] float xRange = 12.5f;
    [Tooltip("in m")] [SerializeField] float yRange = 5.5f;
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = 20f;

    bool isControlEnabled = true;
    
    

    // Update is called once per frame
    void Update () {
        if (isControlEnabled)
        {
            processTranslation();
            processRotation();
        }
        
	}

    private void processRotation()
    {
        
        float pitch = transform.localPosition.y * positionPitchFactor + controlPitchFactor * yThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);


    }

    private void processTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
    
    void OnPlayerDeath()
    {
        isControlEnabled = false;
        print("controls frozen");
    }
}
