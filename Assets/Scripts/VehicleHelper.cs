using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleHelper : MonoBehaviour
{
    [SerializeField] private KeyCode engineKey = KeyCode.F;
    [SerializeField] private Transform steeringWheel;
    [SerializeField] private AudioSource engineIdle;
    [SerializeField] private float maxTurnAngle = 1080f;
    [SerializeField] private RearWheelDrive rearWheelDrive;
    [SerializeField] public bool isEngineOn = false;
    [SerializeField] public bool isInsideCar = false; //Default true

    private void Update()
    {
        if (!isInsideCar) return;

        steeringWheel.localEulerAngles = Vector3.back * Mathf.Clamp((Input.GetAxis("Horizontal") * 100), -maxTurnAngle, maxTurnAngle);
        
        if(Input.GetKeyDown(engineKey))
        {
            isEngineOn = !isEngineOn;
            if(isEngineOn) {
                engineIdle.Play();
                rearWheelDrive.enabled = true;

            } else {
                engineIdle.Stop();
                rearWheelDrive.enabled = false;
            }
        }
    }
}