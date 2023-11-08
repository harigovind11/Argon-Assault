using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based upon player input")]
    [SerializeField]  float controlSpeed = 30f;
    [Tooltip("How far player moves horizontally")]
    [SerializeField]  float xRange = 15f; 
    [Tooltip("How far player moves vertically")]
    [SerializeField]  float yRange = 10f;
    
    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")] 
    [SerializeField] private GameObject[] lasers; 
    
    [Header("Screen position based tuning")]
    [SerializeField]  float positionPitchFactor = 2f;  
    [SerializeField]  float positionYawFactor = 3f;
    
    [Header("Player input based tuning")]
    [SerializeField]  float controlPitchFactor = -10f;
    [SerializeField]  float controlRollFactor = -20f;
    
    float xThrow;
    float yThrow;
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        
        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
         
        float rollDueToControlThrow = xThrow * controlRollFactor;
        
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = yawDueToPosition;
        float roll = rollDueToControlThrow;
        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }
     void ProcessTranslation()
    {
         xThrow = Input.GetAxis("Horizontal");
         yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float yOffset = yThrow * Time.deltaTime * controlSpeed;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);


        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

     void ProcessFiring()
     {
         if (Input.GetButton("Fire1"))
         {
             SetLasersActive(true);
         }
         else
         {
             SetLasersActive(false);
         }
     }
     void SetLasersActive(bool isActive)
     {
         foreach (GameObject laser in lasers)
         {
             var emissionModule = laser.GetComponent<ParticleSystem>().emission;
             emissionModule.enabled = isActive;
         }
     } 
     

}
