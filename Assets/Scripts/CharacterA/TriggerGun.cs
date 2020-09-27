using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGun : MonoBehaviour
{
    [SerializeField] private GameObject fpsGun;
    [SerializeField] private GameObject counterBlock;
    public static bool isActive = false;
    private bool doOnce = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player") && isActive && doOnce)
        {
            fpsGun.SetActive(true);
            counterBlock.SetActive(false);
            doOnce = false;
            this.enabled = false;
        }
    }
}
