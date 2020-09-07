using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterVehicle : MonoBehaviour
{
    [SerializeField] private VehicleSwap vehicleSwap;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            vehicleSwap.SwapPositions(true);
        }
    }
}
