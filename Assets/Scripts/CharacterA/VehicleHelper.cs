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
    [SerializeField] private WheelCollider rr;
    [SerializeField] private WheelCollider rl;
    private int leaveCheck;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rr.motorTorque = rearWheelDrive.maxTorque * rb.mass/2f;
        rl.motorTorque = rearWheelDrive.maxTorque * rb.mass/2f;
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
                rearWheelDrive.canAccelerate = true;
                GetComponent<Rigidbody>().isKinematic = false;

            } else {
                engineIdle.Stop();
                rearWheelDrive.canAccelerate = false;
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        if (Input.GetKeyDown(KeyBinds.exit))
        {
            if (isEngineOn == false && canLeaveCar == true)
            {
                vehicleSwap.SwapPositions(false);
                if (leaveCheck == 0)
                {
                    StoryHandlerA.instance.gameObject.GetComponent<CheckWallet>().enabled = true;
                    leaveCheck++;
                    return;
                }
                if (leaveCheck == 1)
                {
                    StoryHandlerA.instance.gameObject.GetComponent<BoardPlan>().PlanningPhase();
                    leaveCheck++;
                }
            }
        }

        if(Input.GetKeyDown(KeyBinds.horn))
        {
            AudioManager.instance.PlayAudio(honk, 1.0f);
        }
    }
}