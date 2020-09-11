using Aura2API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSwap : MonoBehaviour
{
    [SerializeField] private GameObject characterA;
    [SerializeField] private Transform exitPos;
    [SerializeField] private GameObject vehicleCamera;
    [SerializeField] private GameObject characterCamera;
    [SerializeField] private VehicleHelper vehicleHelper;
    [SerializeField] public bool isLocked = false;
    [SerializeField] private GameObject[] realWheels;
    [SerializeField] private GameObject[] driveWheels;
    [SerializeField] private Light[] lights;
    private AuraCamera fpsAura;
    private AuraCamera carAura;

    private void Start()
    {
        fpsAura = characterCamera.GetComponent<AuraCamera>();
        carAura = vehicleCamera.GetComponent<AuraCamera>();
    }

    public void SwapPositions(bool isInsideCar)
    {
        if (isLocked) return;

        if (isInsideCar) 
        {
            characterA.transform.parent = exitPos;
            vehicleCamera.SetActive(true);
            characterCamera.SetActive(false);
            characterA.SetActive(false);
            vehicleHelper.isInsideCar = true;
            carAura.enabled = true;
            fpsAura.enabled = false;

            foreach (GameObject wheel in realWheels) 
            {
                wheel.SetActive(false);
            }
            foreach (GameObject wheel in driveWheels) 
            {
                wheel.SetActive(true);
            }
            foreach (Light light in lights)
            {
                light.gameObject.SetActive(true);
            }
        } 
        else 
        {
            characterA.transform.parent = null;
            characterA.SetActive(true);
            characterCamera.SetActive(true);
            vehicleCamera.SetActive(false);
            vehicleHelper.isInsideCar = false;
            fpsAura.enabled = true;
            carAura.enabled = false;

            foreach (GameObject wheel in realWheels) 
            {
                wheel.SetActive(true);
            }
            foreach (GameObject wheel in driveWheels)
            {
                wheel.SetActive(false);
            }
            foreach (Light light in lights)
            {
                light.gameObject.SetActive(false);
            }
        }
    }
}
