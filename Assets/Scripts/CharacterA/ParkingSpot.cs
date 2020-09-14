using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingSpot : MonoBehaviour
{
    [SerializeField] private VehicleHelper helper;
    [SerializeField] private Tutorial tutorial;
    private bool instruct = true;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == helper.gameObject)
        {
            helper.canLeaveCar = true;
            if(instruct == true)
            {
                tutorial.FadeKeys(tutorial.engineKey, 3f);
                tutorial.FadeKeys(tutorial.exitKey, 3f);
                instruct = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == helper.gameObject)
        {
            helper.canLeaveCar = false;
        }
    }
}