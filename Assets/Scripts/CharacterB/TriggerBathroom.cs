using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBathroom : MonoBehaviour
{
    [SerializeField] private Door bathroomDoor;
    [SerializeField] private GameObject criminal;
    private bool doOnce = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player") && doOnce)
        {
            bathroomDoor.isOpen = true;
            bathroomDoor.OpenDoor();
            bathroomDoor.isLocked = true;
            bathroomDoor.sPlayBreakThrough = true;
            criminal.SetActive(false);
            doOnce = false;
        }
    }
}
