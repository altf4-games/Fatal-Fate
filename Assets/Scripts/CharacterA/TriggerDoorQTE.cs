using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorQTE : MonoBehaviour
{
    [SerializeField] private Door bathroomDoor;
    private bool doOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("NPC_S") && doOnce == false)
        {
            bathroomDoor.canStopSlowDown = true;
            TimeLimiter.instance.SetupTimeLimiter(10, "Close The Door", SlowDownGame);
            doOnce = true;
        }
    }

    private string SlowDownGame()
    {
        Time.timeScale = .5f;
        return null;
    }
}
