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
    [SerializeField] private AudioSource doorBang;
    [SerializeField] private float openSpeed = 1f;
    [SerializeField] public bool isLocked = false;
    [SerializeField] public bool canBeLockpicked = false;
    [HideInInspector] public bool isOpen = false;
    [HideInInspector] public bool canStopSlowDown = false;

    public void OpenDoor()
    {
        if (canStopSlowDown) {
            StopSlowDown();
        }
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
            if(canBeLockpicked)
            {
                GetComponent<Lockpick>().SetupLockpick();
            }

            if(doorLocked != null)
            {
                AudioManager.instance.PlayAudio(doorLocked, 1.0f);
            }
        }
    }

    private void StopSlowDown()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        TimeLimiter.instance.StopTimer();
        Time.timeScale = 1f;
        Invoke("WaitForDoorClose", 1.25f);
    }

    private void WaitForDoorClose()
    {
        DoorBang();
        doorBang.Play();
        StoryHandlerA.instance.PrintSubtitle(13);
        TriggerGun.isActive = true;
    }

    private void DoorBang()
    {
        LeanTween.moveLocalX(gameObject, -0.072f, .25f).setOnComplete(() =>
        {
            LeanTween.moveLocalX(gameObject, -0.0304004f, .1f).setOnComplete(() =>
            {
                DoorBang();
            });
        });
    }
}