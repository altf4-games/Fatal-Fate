using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class VehicleDoor : MonoBehaviour
{
    [SerializeField] private VehicleSwap vehicleSwap;
    [SerializeField] private GameObject mouseIndicator;

    public void EnterVehicleDoor()
    {
        vehicleSwap.SwapPositions(true);
        mouseIndicator.SetActive(false);
        Destroy(gameObject);
    }
}
