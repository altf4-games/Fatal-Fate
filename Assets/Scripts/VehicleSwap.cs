using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSwap : MonoBehaviour
{
    [SerializeField] private KeyCode exitCar = KeyCode.Escape;
    [SerializeField] private GameObject characterA;
    [SerializeField] private Transform exitPos;
    [SerializeField] private GameObject vehicleCamera;
    [SerializeField] private GameObject characterCamera;
    [SerializeField] private VehicleHelper vehicleHelper;
    [SerializeField] public bool isLocked = false;
    [SerializeField] private GameObject[] realWheels;
    [SerializeField] private GameObject[] driveWheels;

    public void SwapPositions(bool isInsideCar)
    {
        if (isLocked) return;
        if(isInsideCar) 
        {
            characterA.transform.parent = exitPos;
            vehicleCamera.SetActive(true);
            characterCamera.SetActive(false);
            characterA.SetActive(false);
            vehicleHelper.isInsideCar = true;
            foreach (GameObject wheel in realWheels) 
            {
                wheel.SetActive(false);
            }
            foreach (GameObject wheel in driveWheels) 
            {
                wheel.SetActive(true);
            }
        } 
        else 
        {
            characterA.transform.parent = null;
            characterA.SetActive(true);
            characterCamera.SetActive(true);
            vehicleCamera.SetActive(false);
            vehicleHelper.isInsideCar = false;
            foreach (GameObject wheel in realWheels) 
            {
                wheel.SetActive(true);
            }
            foreach (GameObject wheel in driveWheels)
            {
                wheel.SetActive(false);
            }
        }
    }
}
