using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class EndingTrigger : MonoBehaviour
{
    public static bool isActive = false;
    private bool doOnce = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && doOnce && isActive)
        {
            print("End Cutscene Trigger");
            FirstPersonController.pausePlayer = true;
            doOnce = true;
        }
    }
}
