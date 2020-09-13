using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 openRot;
    [SerializeField] private Vector3 closeRot;
    [SerializeField] private GameObject doorRoot;
    [SerializeField] private AudioClip doorHandle;
    [SerializeField] private AudioClip doorLocked;
    [SerializeField] private float openSpeed = 1f;
    [SerializeField] private bool isLocked = false;
    private bool isOpen = false;

    public void OpenDoor()
    {
        if (isLocked == false)
        {
            isOpen = !isOpen;
            if (isOpen == true)
            {
                if(doorHandle != null)
                AudioManager.instance.PlayAudio(doorHandle, 1.0f);
                LeanTween.rotate(doorRoot, openRot, openSpeed);
            }
            else
            {
                if (doorHandle != null)
                AudioManager.instance.PlayAudio(doorHandle, 1.0f);
                LeanTween.rotate(doorRoot, closeRot, openSpeed);
            }
        }
        else
        {
            if (doorLocked != null)
            AudioManager.instance.PlayAudio(doorLocked, 1.0f);
        }
    }
}