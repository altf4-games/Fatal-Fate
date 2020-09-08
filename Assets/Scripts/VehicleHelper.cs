﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleHelper : MonoBehaviour
{

    [SerializeField] private Transform steeringWheel;
    [SerializeField] private AudioSource engineIdle;
    [SerializeField] private AudioClip honk;
    [SerializeField] private float maxTurnAngle = 1080f;
    [SerializeField] private RearWheelDrive rearWheelDrive;
    [SerializeField] private VehicleSwap vehicleSwap;
    [SerializeField] public bool isEngineOn = false;
    [SerializeField] public bool isInsideCar = true;
    [SerializeField] public bool canLeaveCar = false;

    private void Start()
    {
        GameController.SetCursor(false, true);
    }

    private void Update()
    {
        if (!isInsideCar) return;

        steeringWheel.localEulerAngles = Vector3.back * Mathf.Clamp((Input.GetAxis("Horizontal") * 100), -maxTurnAngle, maxTurnAngle);
        
        if(Input.GetKeyDown(KeyBinds.engine))
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

        if (Input.GetKeyDown(KeyBinds.exit))
        {
            if(isEngineOn == false && canLeaveCar == true)
            {
                vehicleSwap.SwapPositions(false);
            }
        }

        if(Input.GetKeyDown(KeyBinds.horn))
        {
            AudioManager.instance.PlayAudio(honk, 1.0f);
        }
    }
}